using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VolgaIT.Models;

namespace VolgaIT.Data
{
    public class DataContext : IdentityDbContext<IdentityUser>
    {
        public DataContext(DbContextOptions<DataContext> opt) : base(opt)
        {
            Database.EnsureCreated();
        }
        public DbSet<App> Apps { get; set; }
        public DbSet<UserApps> UserApps { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<RequestUser> RequestUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<UserApps>().HasKey(x => new { x.UserId, x.AppId });
            //base.OnModelCreating(modelBuilder);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserApps>().HasKey(x=>new {x.UserId, x.AppId});
            modelBuilder.Entity<App>().Property(x => x.DateCreate).HasColumnType("timestamp");
            modelBuilder.Entity<RequestUser>().Property(x => x.CreatedDate).HasColumnType("timestamp");
        }
    }
}