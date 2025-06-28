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
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductHistory> ProductHistories { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<StockHistory> StockHistories { get; set; }

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

            // Category
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("categories");
                entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.Slug).HasMaxLength(100).IsRequired();
                entity.Property(e => e.ImageUrl).HasMaxLength(500);
                entity.HasIndex(e => e.Slug).IsUnique();
                
                // 自己参照のリレーション
                entity.HasOne(e => e.ParentCategory)
                    .WithMany(e => e.ChildCategories)
                    .HasForeignKey(e => e.ParentCategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
                
                // 論理削除のグローバルフィルタ
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            // ProductCategory（多対多の中間テーブル）
            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.ToTable("product_categories");
                entity.HasKey(e => new { e.ProductId, e.CategoryId });
                
                // ProductとProductCategoryのリレーション（1対多）
                entity.HasOne(e => e.Product)
                    .WithMany(p => p.ProductCategories)
                    .HasForeignKey(e => e.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);
                
                // CategoryとProductCategoryのリレーション（1対多）
                entity.HasOne(e => e.Category)
                    .WithMany(c => c.ProductCategories)
                    .HasForeignKey(e => e.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ProductImage
            modelBuilder.Entity<ProductImage>(entity =>
            {
                entity.ToTable("product_images");
                entity.Property(e => e.ImageUrl).HasMaxLength(500).IsRequired();
                entity.Property(e => e.ThumbnailUrl).HasMaxLength(500);
                entity.Property(e => e.Title).HasMaxLength(200);
                entity.Property(e => e.AltText).HasMaxLength(200);
                
                // ProductとProductImageのリレーション（1対多）
                entity.HasOne(e => e.Product)
                    .WithMany(p => p.ProductImages)
                    .HasForeignKey(e => e.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);
                
                // 論理削除のグローバルフィルタ
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            // Product
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("products");
                entity.Property(e => e.Sku).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Name).HasMaxLength(200).IsRequired();
                entity.Property(e => e.Description).HasMaxLength(1000);
                entity.Property(e => e.Price).HasPrecision(10, 2);
                entity.Property(e => e.CostPrice).HasPrecision(10, 2);
                entity.Property(e => e.ListPrice).HasPrecision(10, 2);
                entity.Property(e => e.MetaTitle).HasMaxLength(100);
                entity.Property(e => e.MetaDescription).HasMaxLength(200);
                entity.Property(e => e.Slug).HasMaxLength(200).IsRequired();
                
                entity.HasIndex(e => e.Sku).IsUnique();
                entity.HasIndex(e => e.Slug).IsUnique();
                entity.HasIndex(e => e.Status);
                entity.HasIndex(e => e.IsPublished);
                
                // 論理削除のグローバルフィルタ
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            // ProductHistory
            modelBuilder.Entity<ProductHistory>(entity =>
            {
                entity.ToTable("product_histories");
                entity.Property(e => e.Sku).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Name).HasMaxLength(200).IsRequired();
                entity.Property(e => e.Description).HasMaxLength(1000);
                entity.Property(e => e.Price).HasPrecision(10, 2);
                entity.Property(e => e.CostPrice).HasPrecision(10, 2);
                entity.Property(e => e.ListPrice).HasPrecision(10, 2);
                
                entity.HasIndex(e => e.OriginalId);
                entity.HasIndex(e => e.OperatedAt);
                
                entity.HasOne(e => e.OriginalProduct)
                    .WithMany()
                    .HasForeignKey(e => e.OriginalId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Stock
            modelBuilder.Entity<Stock>(entity =>
            {
                entity.ToTable("stocks");
                entity.HasIndex(e => e.ProductId).IsUnique();
                
                entity.HasOne(e => e.Product)
                    .WithOne(p => p.Stock)
                    .HasForeignKey<Stock>(e => e.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);
                
                // 論理削除のグローバルフィルタ
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            // StockHistory
            modelBuilder.Entity<StockHistory>(entity =>
            {
                entity.ToTable("stock_histories");
                entity.Property(e => e.Note).HasMaxLength(500);
                
                entity.HasIndex(e => e.StockId);
                entity.HasIndex(e => e.OperatedAt);
                entity.HasIndex(e => e.MovementType);
                
                entity.HasOne(e => e.Stock)
                    .WithMany(s => s.StockHistories)
                    .HasForeignKey(e => e.StockId)
                    .OnDelete(DeleteBehavior.Cascade);
                    
                entity.HasOne(e => e.RelatedOrder)
                    .WithMany()
                    .HasForeignKey(e => e.RelatedOrderId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

        }
    }
}
