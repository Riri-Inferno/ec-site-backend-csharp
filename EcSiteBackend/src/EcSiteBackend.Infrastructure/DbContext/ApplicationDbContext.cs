using Microsoft.EntityFrameworkCore;
using EcSiteBackend.Domain.Entities;

namespace EcSiteBackend.Infrastructure.DbContext
{
    public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserHistory> UserHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // テーブル名を指定
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<UserHistory>().ToTable("user_histories");
        }
    }
}
