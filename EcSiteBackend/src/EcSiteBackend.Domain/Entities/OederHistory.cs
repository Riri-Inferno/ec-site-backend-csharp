using EcSiteBackend.Domain.Enums;

namespace EcSiteBackend.Domain.Entities
{
    /// <summary>
    /// 注文変更履歴エンティティ
    /// </summary>
    public class OrderHistory : HistoryEntity
    {
        /// <summary>
        /// 注文番号
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

        // Navigation Property
        /// <summary>
        /// 元の注文エンティティ
        /// </summary>
        public virtual Order? OriginalOrder { get; set; }
    }
}
