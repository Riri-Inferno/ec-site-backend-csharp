namespace EcSiteBackend.Domain.Entities
{
    /// <summary>
    /// 商品とカテゴリの中間テーブル
    /// </summary>
    public class ProductCategory
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// カテゴリID
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        /// 主カテゴリフラグ
        /// </summary>
        public bool IsPrimary { get; set; }

        /// <summary>
        /// 登録日時
        /// </summary>
        public DateTime CreatedAt { get; set; }

        // Navigation Properties
        /// <summary>
        /// 商品
        /// </summary>
        public virtual Product Product { get; set; } = null!;

        /// <summary>
        /// カテゴリ
        /// </summary>
        public virtual Category Category { get; set; } = null!;
    }
}
