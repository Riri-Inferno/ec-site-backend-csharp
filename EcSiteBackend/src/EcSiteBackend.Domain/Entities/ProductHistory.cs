using EcSiteBackend.Domain.Enums;

namespace EcSiteBackend.Domain.Entities
{
    /// <summary>
    /// 商品変更履歴エンティティ
    /// </summary>
    public class ProductHistory : HistoryEntity
    {
        /// <summary>
        /// SKU
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
        /// 商品詳細説明
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
        /// 定価
        /// </summary>
        public decimal? ListPrice { get; set; }

        /// <summary>
        /// 重量
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

        // Navigation Property
        /// <summary>
        /// 元の商品エンティティ
        /// </summary>
        public virtual Product? OriginalProduct { get; set; }
    }
}
