namespace EcSiteBackend.Domain.Entities
{
    /// <summary>
    /// 配送方法マスタ
    /// </summary>
    public class ShippingMethod : BaseEntity
    {
        /// <summary>
        /// 配送方法名
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 配送方法コード
        /// </summary>
        public string Code { get; set; } = string.Empty;

        /// <summary>
        /// 説明
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 基本配送料
        /// </summary>
        public decimal BaseFee { get; set; }

        /// <summary>
        /// 無料配送の最小金額
        /// </summary>
        public decimal? FreeShippingMinAmount { get; set; }

        /// <summary>
        /// 配送日数（目安）
        /// </summary>
        public int EstimatedDays { get; set; }

        /// <summary>
        /// 表示順
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 有効フラグ
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 追跡可能フラグ
        /// </summary>
        public bool IsTrackable { get; set; }

        /// <summary>
        /// 配送業者名
        /// </summary>
        public string? CarrierName { get; set; }

        // Navigation Properties
        /// <summary>
        /// この配送方法を使用した注文のコレクション
        /// </summary>
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        /// <summary>
        /// この配送方法を使用した配送のコレクション
        /// </summary>
        public virtual ICollection<Shipping> Shippings { get; set; } = new List<Shipping>();
    }
}
