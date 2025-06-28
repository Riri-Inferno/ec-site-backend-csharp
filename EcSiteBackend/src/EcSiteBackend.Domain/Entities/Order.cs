using EcSiteBackend.Domain.Enums;

namespace EcSiteBackend.Domain.Entities
{
    /// <summary>
    /// 注文エンティティ
    /// </summary>
    public class Order : BaseEntity
    {
        /// <summary>
        /// 注文番号（表示用）
        /// </summary>
        public string OrderNumber { get; set; } = string.Empty;

        /// <summary>
        /// ユーザーID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 注文日時
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// 注文ステータス
        /// </summary>
        public OrderStatus Status { get; set; }

        /// <summary>
        /// 小計（税抜）
        /// </summary>
        public decimal SubTotal { get; set; }

        /// <summary>
        /// 割引額
        /// </summary>
        public decimal DiscountAmount { get; set; }

        /// <summary>
        /// 税額
        /// </summary>
        public decimal TaxAmount { get; set; }

        /// <summary>
        /// 送料
        /// </summary>
        public decimal ShippingFee { get; set; }

        /// <summary>
        /// 合計金額（税込）
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// 支払い方法ID
        /// </summary>
        public Guid? PaymentMethodId { get; set; }

        /// <summary>
        /// 配送方法ID
        /// </summary>
        public Guid? ShippingMethodId { get; set; }

        /// <summary>
        /// 配送先住所（JSON）
        /// </summary>
        public string ShippingAddress { get; set; } = string.Empty;

        /// <summary>
        /// 請求先住所（JSON）
        /// </summary>
        public string BillingAddress { get; set; } = string.Empty;

        /// <summary>
        /// 顧客メモ
        /// </summary>
        public string? CustomerNote { get; set; }

        /// <summary>
        /// 管理者メモ
        /// </summary>
        public string? AdminNote { get; set; }

        /// <summary>
        /// クーポンコード
        /// </summary>
        public string? CouponCode { get; set; }

        /// <summary>
        /// 支払い期限
        /// </summary>
        public DateTime? PaymentDueDate { get; set; }

        /// <summary>
        /// 支払い完了日時
        /// </summary>
        public DateTime? PaidAt { get; set; }

        /// <summary>
        /// 発送日時
        /// </summary>
        public DateTime? ShippedAt { get; set; }

        /// <summary>
        /// 配達完了日時
        /// </summary>
        public DateTime? DeliveredAt { get; set; }

        /// <summary>
        /// キャンセル日時
        /// </summary>
        public DateTime? CancelledAt { get; set; }

        // Navigation Properties
        /// <summary>
        /// ユーザー
        /// </summary>
        public virtual User User { get; set; } = null!;

        /// <summary>
        /// 注文明細のコレクション
        /// </summary>
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        /// <summary>
        /// 注文ステータス履歴のコレクション
        /// </summary>
        public virtual ICollection<OrderStatusHistory> OrderStatusHistories { get; set; } = new List<OrderStatusHistory>();

        /// <summary>
        /// 支払い情報
        /// </summary>
        public virtual Payment? Payment { get; set; }

        /// <summary>
        /// 配送情報
        /// </summary>
        public virtual Shipping? Shipping { get; set; }

        /// <summary>
        /// 支払い方法
        /// </summary>
        public virtual PaymentMethod? PaymentMethod { get; set; }

        /// <summary>
        /// 配送方法
        /// </summary>
        public virtual ShippingMethod? ShippingMethod { get; set; }
    }
}
