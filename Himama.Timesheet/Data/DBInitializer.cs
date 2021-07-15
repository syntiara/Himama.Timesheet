using System.Linq;
using Himama.Timesheet.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace Himama.Timesheet.Data
{
    public static class DbInitializer
    {
        internal static void Migrate(this DBContext context)
        {
            if (context.Database.GetPendingMigrations().Any())
                context.Database.Migrate();
        }

        public static void Initialize(this DBContext context)
        {
            // Look for any students.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            var users = new User[]
            {
            new User{FirstName = "Carson", LastName = "Alexander", Email = "carlex@gmail.com"},
            new User{FirstName = "Meredith", LastName = "Alonso", Email ="merlon@gmail.com"},
            new User{FirstName = "Arturo", LastName = "Anand", Email ="artna@gmail.com"},
            new User{FirstName = "Gytis", LastName = "Barzdukas", Email ="gytar@gmail.com"},
            new User{FirstName = "Yan", LastName = "Li", Email ="yanli@gmail.com"},
            new User{FirstName = "Peggy", LastName = "Justice", Email ="pegus@gmail.com"},
            new User{FirstName = "Laura", LastName = "Norman", Email ="lauor@gmail.com"},
            new User{FirstName = "Nino", LastName = "Olivetto", Email ="ninli@gmail.com"}
            };
            foreach (User s in users)
            {
                context.Users.Add(s);
            }
            context.SaveChanges();
        }
    }
}
