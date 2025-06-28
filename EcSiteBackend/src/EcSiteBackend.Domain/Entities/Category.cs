namespace EcSiteBackend.Domain.Entities
{
    /// <summary>
    /// 商品カテゴリマスタ
    /// </summary>
    public class Category : BaseEntity
    {
        /// <summary>
        /// カテゴリ名
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// カテゴリ説明
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// スラッグ（URL用）
        /// </summary>
        public string Slug { get; set; } = string.Empty;

        /// <summary>
        /// 親カテゴリID
        /// </summary>
        public Guid? ParentCategoryId { get; set; }

        /// <summary>
        /// 表示順
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// アクティブフラグ
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// カテゴリ画像URL
        /// </summary>
        public string? ImageUrl { get; set; }

        // Navigation Properties
        /// <summary>
        /// 親カテゴリ
        /// </summary>
        public virtual Category? ParentCategory { get; set; }

        /// <summary>
        /// 子カテゴリのコレクション
        /// </summary>
        public virtual ICollection<Category> ChildCategories { get; set; } = new List<Category>();

        /// <summary>
        /// 商品カテゴリのコレクション
        /// </summary>
        public virtual ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();

        /// <summary>
        /// フルパス（親カテゴリ含む）を取得
        /// </summary>
        public string GetFullPath(string separator = " > ")
        {
            var path = Name;
            var parent = ParentCategory;
            while (parent != null)
            {
                path = $"{parent.Name}{separator}{path}";
                parent = parent.ParentCategory;
            }
            return path;
        }
    }
}
