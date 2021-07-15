using System;
using Himama.Timesheet;
using Himama.Timesheet.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Himama.Timesheet.Extensions
{
    public static class DBHelper
    {
        public static bool IsProdEnv()
        {
            return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production";
        }

        public static string GetConnectionString(this IConfiguration configuration)
        {
            string dbConnection;
            if (!IsProdEnv())
                dbConnection = configuration.GetConnectionString("DATABASE_URL");
            else
            {
                // Use connection string provided at runtime by Heroku.
                var connUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
                // Parse connection URL to connection string for Npgsql
                connUrl = connUrl.Replace("postgres://", string.Empty);
                var pgUserPass = connUrl.Split("@")[0];
                var pgHostPortDb = connUrl.Split("@")[1];
                var pgHostPort = pgHostPortDb.Split("/")[0];
                var pgDb = pgHostPortDb.Split("/")[1];
                var pgUser = pgUserPass.Split(":")[0];
                var pgPass = pgUserPass.Split(":")[1];
                var pgHost = pgHostPort.Split(":")[0];
                var pgPort = pgHostPort.Split(":")[1];
                dbConnection = $"Server={pgHost};Port={pgPort};User Id={pgUser};Password={pgPass};Database={pgDb};Pooling=true;SSL Mode=Prefer;Trust Server Certificate=true;";
            }

            return dbConnection;
        }

        public static IHost MigrateAndSeedDb(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            using (var context = scope.ServiceProvider.GetService<DBContext>())
            {
                try
                {
                    context.Migrate();
                    context.Initialize();
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating the DB.");
                }
            }

            return host;
        }
    }
}