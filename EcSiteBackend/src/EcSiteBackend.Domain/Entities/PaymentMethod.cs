namespace EcSiteBackend.Domain.Entities
{
    /// <summary>
    /// 支払い方法マスタ
    /// </summary>
    public class PaymentMethod : BaseEntity
    {
        /// <summary>
        /// 支払い方法名
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 支払い方法コード
        /// </summary>
        public string Code { get; set; } = string.Empty;

        /// <summary>
        /// 説明
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 表示順
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 有効フラグ
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 手数料（固定額）
        /// </summary>
        public decimal FeeAmount { get; set; }

        /// <summary>
        /// 手数料（率）
        /// </summary>
        public decimal FeeRate { get; set; }

        /// <summary>
        /// 最小利用金額
        /// </summary>
        public decimal? MinimumAmount { get; set; }

        /// <summary>
        /// 最大利用金額
        /// </summary>
        public decimal? MaximumAmount { get; set; }

        /// <summary>
        /// アイコンURL
        /// </summary>
        public string? IconUrl { get; set; }

        // Navigation Properties
        /// <summary>
        /// この支払い方法を使用した注文のコレクション
        /// </summary>
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        /// <summary>
        /// この支払い方法を使用した支払いのコレクション
        /// </summary>
        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
