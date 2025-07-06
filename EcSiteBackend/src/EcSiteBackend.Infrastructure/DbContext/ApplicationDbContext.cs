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
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderStatusHistory> OrderStatusHistories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderHistory> OrderHistories { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentHistory> PaymentHistories { get; set; }
        public DbSet<ShippingMethod> ShippingMethods { get; set; }
        public DbSet<Shipping> Shippings { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<UserCoupon> UserCoupons { get; set; }
        public DbSet<TaxRate> TaxRates { get; set; }
        public DbSet<SystemSetting> SystemSettings { get; set; }

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

            // Cart
            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("carts");
                entity.Property(e => e.SessionId).HasMaxLength(100);

                // UserとCartの1対1リレーション
                entity.HasOne(e => e.User)
                    .WithOne(u => u.Cart)
                    .HasForeignKey<Cart>(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(e => e.UserId).IsUnique();
                entity.HasIndex(e => e.SessionId);
                entity.HasIndex(e => e.LastActivityAt);

                // 論理削除のグローバルフィルタ
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            // CartItem
            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.ToTable("cart_items");
                entity.Property(e => e.PriceAtAdded).HasPrecision(10, 2);

                // CartとCartItemのリレーション（1対多）
                entity.HasOne(e => e.Cart)
                    .WithMany(c => c.CartItems)
                    .HasForeignKey(e => e.CartId)
                    .OnDelete(DeleteBehavior.Cascade);

                // ProductとCartItemのリレーション（1対多）
                entity.HasOne(e => e.Product)
                    .WithMany(p => p.CartItems)
                    .HasForeignKey(e => e.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);

                // 同じ商品を重複して追加できないようにする
                entity.HasIndex(e => new { e.CartId, e.ProductId }).IsUnique();

                // 論理削除のグローバルフィルタ
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            // OrderItem
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.ToTable("order_items");
                entity.Property(e => e.ProductName).HasMaxLength(200).IsRequired();
                entity.Property(e => e.ProductSku).HasMaxLength(50).IsRequired();
                entity.Property(e => e.UnitPrice).HasPrecision(10, 2);
                entity.Property(e => e.DiscountAmount).HasPrecision(10, 2);
                entity.Property(e => e.TaxAmount).HasPrecision(10, 2);

                entity.HasOne(e => e.Order)
                    .WithMany(o => o.OrderItems)
                    .HasForeignKey(e => e.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Product)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(e => e.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);

                // 論理削除のグローバルフィルタ
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            // OrderStatusHistory
            modelBuilder.Entity<OrderStatusHistory>(entity =>
            {
                entity.ToTable("order_status_histories");
                entity.Property(e => e.Reason).HasMaxLength(200);
                entity.Property(e => e.Note).HasMaxLength(500);

                entity.HasOne(e => e.Order)
                    .WithMany(o => o.OrderStatusHistories)
                    .HasForeignKey(e => e.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(e => e.OrderId);
                entity.HasIndex(e => e.CreatedAt);

                // 論理削除のグローバルフィルタ
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");
                entity.Property(e => e.OrderNumber).HasMaxLength(50).IsRequired();
                entity.Property(e => e.SubTotal).HasPrecision(10, 2);
                entity.Property(e => e.DiscountAmount).HasPrecision(10, 2);
                entity.Property(e => e.TaxAmount).HasPrecision(10, 2);
                entity.Property(e => e.ShippingFee).HasPrecision(10, 2);
                entity.Property(e => e.TotalAmount).HasPrecision(10, 2);
                entity.Property(e => e.CustomerNote).HasMaxLength(500);
                entity.Property(e => e.AdminNote).HasMaxLength(500);
                entity.Property(e => e.CouponCode).HasMaxLength(50);

                entity.HasIndex(e => e.OrderNumber).IsUnique();
                entity.HasIndex(e => e.UserId);
                entity.HasIndex(e => e.OrderDate);
                entity.HasIndex(e => e.Status);

                entity.HasOne(e => e.User)
                    .WithMany(u => u.Orders)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.PaymentMethod)
                    .WithMany()
                    .HasForeignKey(e => e.PaymentMethodId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(e => e.ShippingMethod)
                    .WithMany()
                    .HasForeignKey(e => e.ShippingMethodId)
                    .OnDelete(DeleteBehavior.SetNull);

                // 論理削除のグローバルフィルタ
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            // OrderHistory
            modelBuilder.Entity<OrderHistory>(entity =>
            {
                entity.ToTable("order_histories");
                entity.Property(e => e.OrderNumber).HasMaxLength(50).IsRequired();
                entity.Property(e => e.SubTotal).HasPrecision(10, 2);
                entity.Property(e => e.DiscountAmount).HasPrecision(10, 2);
                entity.Property(e => e.TaxAmount).HasPrecision(10, 2);
                entity.Property(e => e.ShippingFee).HasPrecision(10, 2);
                entity.Property(e => e.TotalAmount).HasPrecision(10, 2);
                entity.Property(e => e.CustomerNote).HasMaxLength(500);
                entity.Property(e => e.AdminNote).HasMaxLength(500);

                entity.HasIndex(e => e.OriginalId);
                entity.HasIndex(e => e.OperatedAt);

                entity.HasOne(e => e.OriginalOrder)
                    .WithMany()
                    .HasForeignKey(e => e.OriginalId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // PaymentMethod
            modelBuilder.Entity<PaymentMethod>(entity =>
            {
                entity.ToTable("payment_methods");
                entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Code).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.FeeAmount).HasPrecision(10, 2);
                entity.Property(e => e.FeeRate).HasPrecision(5, 4);
                entity.Property(e => e.MinimumAmount).HasPrecision(10, 2);
                entity.Property(e => e.MaximumAmount).HasPrecision(10, 2);
                entity.Property(e => e.IconUrl).HasMaxLength(500);

                entity.HasIndex(e => e.Code).IsUnique();
                entity.HasIndex(e => e.IsActive);

                // 論理削除のグローバルフィルタ
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            // Payment
            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("payments");
                entity.Property(e => e.Amount).HasPrecision(10, 2);
                entity.Property(e => e.RefundedAmount).HasPrecision(10, 2);
                entity.Property(e => e.TransactionId).HasMaxLength(100);
                entity.Property(e => e.ProviderName).HasMaxLength(50);
                entity.Property(e => e.FailureReason).HasMaxLength(500);
                entity.Property(e => e.RefundReason).HasMaxLength(500);
                entity.Property(e => e.Note).HasMaxLength(500);

                entity.HasIndex(e => e.OrderId);
                entity.HasIndex(e => e.Status);
                entity.HasIndex(e => e.TransactionId);

                entity.HasOne(e => e.Order)
                    .WithOne(o => o.Payment)
                    .HasForeignKey<Payment>(e => e.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.PaymentMethod)
                    .WithMany(pm => pm.Payments)
                    .HasForeignKey(e => e.PaymentMethodId)
                    .OnDelete(DeleteBehavior.Restrict);

                // 論理削除のグローバルフィルタ
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            // PaymentHistory
            modelBuilder.Entity<PaymentHistory>(entity =>
            {
                entity.ToTable("payment_histories");
                entity.Property(e => e.Amount).HasPrecision(10, 2);
                entity.Property(e => e.RefundAmount).HasPrecision(10, 2);
                entity.Property(e => e.TransactionId).HasMaxLength(100);
                entity.Property(e => e.ErrorMessage).HasMaxLength(500);

                entity.HasIndex(e => e.PaymentId);
                entity.HasIndex(e => e.OperatedAt);

                entity.HasOne(e => e.Payment)
                    .WithMany(p => p.PaymentHistories)
                    .HasForeignKey(e => e.PaymentId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // ShippingMethod
            modelBuilder.Entity<ShippingMethod>(entity =>
            {
                entity.ToTable("shipping_methods");
                entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Code).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.BaseFee).HasPrecision(10, 2);
                entity.Property(e => e.FreeShippingMinAmount).HasPrecision(10, 2);
                entity.Property(e => e.CarrierName).HasMaxLength(100);

                entity.HasIndex(e => e.Code).IsUnique();
                entity.HasIndex(e => e.IsActive);

                // 論理削除のグローバルフィルタ
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            // Shipping
            modelBuilder.Entity<Shipping>(entity =>
            {
                entity.ToTable("shippings");
                entity.Property(e => e.TrackingNumber).HasMaxLength(100);
                entity.Property(e => e.CarrierName).HasMaxLength(100);
                entity.Property(e => e.ShippingFee).HasPrecision(10, 2);
                entity.Property(e => e.Note).HasMaxLength(500);

                entity.HasIndex(e => e.OrderId).IsUnique();
                entity.HasIndex(e => e.TrackingNumber);
                entity.HasIndex(e => e.Status);

                entity.HasOne(e => e.Order)
                    .WithOne(o => o.Shipping)
                    .HasForeignKey<Shipping>(e => e.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.ShippingMethod)
                    .WithMany(sm => sm.Shippings)
                    .HasForeignKey(e => e.ShippingMethodId)
                    .OnDelete(DeleteBehavior.Restrict);

                // 論理削除のグローバルフィルタ
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            // Review
            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("reviews");
                entity.Property(e => e.Title).HasMaxLength(200);
                entity.Property(e => e.Comment).HasMaxLength(2000).IsRequired();
                entity.Property(e => e.Rating).HasDefaultValue(1);

                entity.HasIndex(e => new { e.ProductId, e.UserId }).IsUnique();
                entity.HasIndex(e => e.IsApproved);
                entity.HasIndex(e => e.Rating);

                entity.HasOne(e => e.User)
                    .WithMany(u => u.Reviews)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Product)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(e => e.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Order)
                    .WithMany()
                    .HasForeignKey(e => e.OrderId)
                    .OnDelete(DeleteBehavior.SetNull);

                // 論理削除のグローバルフィルタ
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            // Favorite
            modelBuilder.Entity<Favorite>(entity =>
            {
                entity.ToTable("favorites");
                entity.Property(e => e.Note).HasMaxLength(500);

                entity.HasIndex(e => new { e.UserId, e.ProductId }).IsUnique();
                entity.HasIndex(e => e.AddedAt);

                entity.HasOne(e => e.User)
                    .WithMany(u => u.Favorites)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Product)
                    .WithMany(p => p.Favorites)
                    .HasForeignKey(e => e.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);

                // 論理削除のグローバルフィルタ
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            // Coupon
            modelBuilder.Entity<Coupon>(entity =>
            {
                entity.ToTable("coupons");
                entity.Property(e => e.Code).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.DiscountAmount).HasPrecision(10, 2);
                entity.Property(e => e.DiscountRate).HasPrecision(5, 2);
                entity.Property(e => e.MaxDiscountAmount).HasPrecision(10, 2);
                entity.Property(e => e.MinimumPurchaseAmount).HasPrecision(10, 2);

                entity.HasIndex(e => e.Code).IsUnique();
                entity.HasIndex(e => e.ValidFrom);
                entity.HasIndex(e => e.ValidTo);

                entity.HasOne(e => e.TargetCategory)
                    .WithMany()
                    .HasForeignKey(e => e.TargetCategoryId)
                    .OnDelete(DeleteBehavior.SetNull);

                // 論理削除のグローバルフィルタ
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            // UserCoupon
            modelBuilder.Entity<UserCoupon>(entity =>
            {
                entity.ToTable("user_coupons");

                entity.HasIndex(e => new { e.UserId, e.CouponId });
                entity.HasIndex(e => e.IsUsed);

                entity.HasOne(e => e.User)
                    .WithMany(u => u.UserCoupons)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Coupon)
                    .WithMany(c => c.UserCoupons)
                    .HasForeignKey(e => e.CouponId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.UsedOrder)
                    .WithMany()
                    .HasForeignKey(e => e.UsedOrderId)
                    .OnDelete(DeleteBehavior.SetNull);

                // 論理削除のグローバルフィルタ
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            // TaxRate
            modelBuilder.Entity<TaxRate>(entity =>
            {
                entity.ToTable("tax_rates");
                entity.Property(e => e.Name).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Rate).HasPrecision(5, 2);
                entity.Property(e => e.Description).HasMaxLength(200);

                entity.HasIndex(e => e.TaxType);
                entity.HasIndex(e => e.EffectiveFrom);
                entity.HasIndex(e => e.IsDefault);

                // 論理削除のグローバルフィルタ
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            // SystemSetting
            modelBuilder.Entity<SystemSetting>(entity =>
            {
                entity.ToTable("system_settings");
                entity.Property(e => e.Key).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Value).HasMaxLength(1000).IsRequired();
                entity.Property(e => e.Category).HasMaxLength(50).IsRequired();
                entity.Property(e => e.DataType).HasMaxLength(20).IsRequired();
                entity.Property(e => e.Description).HasMaxLength(500);

                entity.HasIndex(e => e.Key).IsUnique();
                entity.HasIndex(e => e.Category);

                // 論理削除のグローバルフィルタ
                entity.HasQueryFilter(e => !e.IsDeleted);
            });
            
            // LoginHistory
            modelBuilder.Entity<LoginHistory>(entity =>
            {
                entity.ToTable("login_histories");
                entity.Property(e => e.Email).HasMaxLength(256).IsRequired();
                entity.Property(e => e.IpAddress).HasMaxLength(45); // IPv6対応
                entity.Property(e => e.UserAgent).HasMaxLength(500);
                entity.Property(e => e.FailureReason).HasMaxLength(200);
                entity.Property(e => e.DeviceInfo).HasMaxLength(100);
                entity.Property(e => e.Browser).HasMaxLength(100);
                
                entity.HasIndex(e => e.Email);
                entity.HasIndex(e => e.AttemptedAt);
                entity.HasIndex(e => e.IsSuccess);
                
                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.SetNull);
            });
        }
    }
}
