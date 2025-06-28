namespace EcSiteBackend.Domain.Entities
{
    /// <summary>
    /// 商品エンティティ
    /// </summary>
    public class Product : BaseEntity
    {
        /// <summary>
        /// SKU（Stock Keeping Unit）
        /// </summary>
        public string Sku { get; set; } = string.Empty;

        /// <summary>
        /// 商品名
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 商品説明
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 商品詳細説明（HTML可）
        /// </summary>
        public string? DetailDescription { get; set; }

        /// <summary>
        /// 販売価格（税抜）
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 原価
        /// </summary>
        public decimal? CostPrice { get; set; }

        /// <summary>
        /// 定価（希望小売価格）
        /// </summary>
        public decimal? ListPrice { get; set; }

        /// <summary>
        /// 重量（グラム）
        /// </summary>
        public int? Weight { get; set; }

        /// <summary>
        /// 販売ステータス
        /// </summary>
        public ProductStatus Status { get; set; }

        /// <summary>
        /// 公開フラグ
        /// </summary>
        public bool IsPublished { get; set; }

        /// <summary>
        /// 公開開始日時
        /// </summary>
        public DateTime? PublishedAt { get; set; }

        /// <summary>
        /// メタタイトル（SEO用）
        /// </summary>
        public string? MetaTitle { get; set; }

        /// <summary>
        /// メタ説明（SEO用）
        /// </summary>
        public string? MetaDescription { get; set; }

        /// <summary>
        /// スラッグ（URL用）
        /// </summary>
        public string Slug { get; set; } = string.Empty;

        // Navigation Properties
        /// <summary>
        /// 商品カテゴリのコレクション
        /// </summary>
        public virtual ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();

        /// <summary>
        /// 商品画像のコレクション
        /// </summary>
        public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

        /// <summary>
        /// 在庫情報
        /// </summary>
        public virtual Stock? Stock { get; set; }

        /// <summary>
        /// 注文明細のコレクション
        /// </summary>
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        /// <summary>
        /// レビューのコレクション
        /// </summary>
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

        /// <summary>
        /// お気に入りのコレクション
        /// </summary>
        public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

        /// <summary>
        /// カート内商品のコレクション
        /// </summary>
        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
