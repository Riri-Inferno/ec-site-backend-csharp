using EcSiteBackend.Domain.Enums;


namespace EcSiteBackend.Domain.Entities
{
    /// <summary>
    /// 注文明細
    /// </summary>
    public class OrderItem : BaseEntity
    {
        /// <summary>
        /// 注文ID
        /// </summary>
        public Guid OrderId { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// 商品名（注文時点）
        /// </summary>
        public string ProductName { get; set; } = string.Empty;

        /// <summary>
        /// 商品SKU（注文時点）
        /// </summary>
        public string ProductSku { get; set; } = string.Empty;

        /// <summary>
        /// 単価（税抜）
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// 割引額
        /// </summary>
        public decimal DiscountAmount { get; set; }

        /// <summary>
        /// 税額
        /// </summary>
        public decimal TaxAmount { get; set; }

        /// <summary>
        /// 小計（税抜）
        /// </summary>
        public decimal SubTotal => UnitPrice * Quantity - DiscountAmount;

        /// <summary>
        /// 小計（税込）
        /// </summary>
        public decimal TotalAmount => SubTotal + TaxAmount;

        // Navigation Properties
        /// <summary>
        /// 注文
        /// </summary>
        public virtual Order Order { get; set; } = null!;

        /// <summary>
        /// 商品
        /// </summary>
        public virtual Product Product { get; set; } = null!;
    }
}
