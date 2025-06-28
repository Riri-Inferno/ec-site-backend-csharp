using EcSiteBackend.Domain.Enums;

namespace EcSiteBackend.Domain.Entities
{
    /// <summary>
    /// 配送情報エンティティ
    /// </summary>
    public class Shipping : BaseEntity
    {
        /// <summary>
        /// 注文ID
        /// </summary>
        public Guid OrderId { get; set; }

        /// <summary>
        /// 配送方法ID
        /// </summary>
        public Guid ShippingMethodId { get; set; }

        /// <summary>
        /// 配送ステータス
        /// </summary>
        public ShippingStatus Status { get; set; }

        /// <summary>
        /// 追跡番号
        /// </summary>
        public string? TrackingNumber { get; set; }

        /// <summary>
        /// 配送業者名
        /// </summary>
        public string? CarrierName { get; set; }

        /// <summary>
        /// 配送料
        /// </summary>
        public decimal ShippingFee { get; set; }

        /// <summary>
        /// 発送予定日
        /// </summary>
        public DateTime? EstimatedShipDate { get; set; }

        /// <summary>
        /// 配達予定日
        /// </summary>
        public DateTime? EstimatedDeliveryDate { get; set; }

        /// <summary>
        /// 実際の発送日時
        /// </summary>
        public DateTime? ActualShipDate { get; set; }

        /// <summary>
        /// 実際の配達日時
        /// </summary>
        public DateTime? ActualDeliveryDate { get; set; }

        /// <summary>
        /// 配送先住所（JSON）
        /// </summary>
        public string ShippingAddress { get; set; } = string.Empty;

        /// <summary>
        /// 備考
        /// </summary>
        public string? Note { get; set; }

        // Navigation Properties
        /// <summary>
        /// 注文
        /// </summary>
        public virtual Order Order { get; set; } = null!;

        /// <summary>
        /// 配送方法
        /// </summary>
        public virtual ShippingMethod ShippingMethod { get; set; } = null!;
    }
}
