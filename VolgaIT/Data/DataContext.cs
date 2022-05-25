using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VolgaIT.Models;

namespace VolgaIT.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> opt) : base(opt)
        {
            Database.EnsureCreated();
        }
        public DbSet<App> Apps { get; set; }
        public DbSet<UserApp> UsersApps { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<RequestUser> RequestUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserApp>().HasKey(x => new { x.UsersId, x.AppsId });
            modelBuilder.Entity<App>().Property(x => x.DateCreate).HasColumnType("timestamp");
            modelBuilder.Entity<RequestUser>().Property(x => x.CreatedDate).HasColumnType("timestamp");
        }
    }
}