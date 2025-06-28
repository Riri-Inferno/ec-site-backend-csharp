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
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<PasswordResetToken> PasswordResetTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.Email).HasMaxLength(256).IsRequired();
                entity.Property(e => e.FirstName).HasMaxLength(50).IsRequired();
                entity.Property(e => e.LastName).HasMaxLength(50).IsRequired();
                entity.Property(e => e.PhoneNumber).HasMaxLength(20);

                // 論理削除のグローバルフィルタ
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            // UserHistory
            modelBuilder.Entity<UserHistory>(entity =>
            {
                entity.ToTable("user_histories");
                entity.HasIndex(e => e.OriginalId);
                entity.Property(e => e.Email).HasMaxLength(256).IsRequired();
                entity.Property(e => e.FirstName).HasMaxLength(50).IsRequired();
                entity.Property(e => e.LastName).HasMaxLength(50).IsRequired();
                entity.Property(e => e.PhoneNumber).HasMaxLength(20);

                // UserHistoryとUserのリレーション（1対多）
                entity.HasOne(e => e.OriginalUser)
                    .WithMany()
                    .HasForeignKey(e => e.OriginalId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Role
            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("roles");
                entity.HasIndex(e => e.Name).IsUnique();
                entity.Property(e => e.Name).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Description).HasMaxLength(200);

                // 論理削除のグローバルフィルタ
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            // UserRole（多対多の中間テーブル）
            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("user_roles");

                // 複合主キーの設定
                entity.HasKey(e => new { e.UserId, e.RoleId });

                // UserとUserRoleのリレーション（1対多）
                entity.HasOne(e => e.User)
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                // RoleとUserRoleのリレーション（1対多）    
                entity.HasOne(e => e.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(e => e.RoleId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // UserAddress
            modelBuilder.Entity<UserAddress>(entity =>
            {
                entity.ToTable("user_addresses");
                entity.Property(e => e.AddressName).HasMaxLength(50);
                entity.Property(e => e.RecipientName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.PostalCode).HasMaxLength(7).IsRequired();
                entity.Property(e => e.Prefecture).HasMaxLength(10).IsRequired();
                entity.Property(e => e.City).HasMaxLength(50).IsRequired();
                entity.Property(e => e.AddressLine1).HasMaxLength(100).IsRequired();
                entity.Property(e => e.AddressLine2).HasMaxLength(100);
                entity.Property(e => e.PhoneNumber).HasMaxLength(20).IsRequired();
                
                // UserとUserAddressのリレーション（1対多）
                entity.HasOne(e => e.User)
                    .WithMany(u => u.UserAddresses)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
                
                // 論理削除のグローバルフィルタ
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            // PasswordResetToken
            modelBuilder.Entity<PasswordResetToken>(entity =>
            {
                entity.ToTable("password_reset_tokens");
                entity.Property(e => e.TokenHash).HasMaxLength(256).IsRequired();
                entity.Property(e => e.RequestIpAddress).HasMaxLength(45);
                entity.Property(e => e.UsedIpAddress).HasMaxLength(45);
                entity.HasIndex(e => e.TokenHash);
                entity.HasIndex(e => new { e.UserId, e.ExpiresAt });
                
                // UserとPasswordResetTokenのリレーション（1対多）
                entity.HasOne(e => e.User)
                    .WithMany(u => u.PasswordResetTokens)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
                
                // 論理削除のグローバルフィルタ
                entity.HasQueryFilter(e => !e.IsDeleted);
            });
        }
    }
}
