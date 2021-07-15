using Himama.Timesheet.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace Himama.Timesheet.Data
{
    public class DBContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<UserAttendance> UserAttendance { get; set; }

        public DBContext() { }

        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User").
            Property(t => t.Email).HasMaxLength(50);

            modelBuilder.Entity<User>().ToTable("User").
            Property(t => t.FirstName).HasMaxLength(50);

            modelBuilder.Entity<User>().ToTable("User").
            Property(t => t.LastName).HasMaxLength(50);
        }
    }
}