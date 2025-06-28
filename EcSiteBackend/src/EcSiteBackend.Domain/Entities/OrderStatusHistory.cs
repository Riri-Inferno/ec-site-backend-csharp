using EcSiteBackend.Domain.Enums;

namespace EcSiteBackend.Domain.Entities
{
    /// <summary>
    /// 注文ステータス変更履歴
    /// </summary>
    public class OrderStatusHistory : BaseEntity
    {
        /// <summary>
        /// 注文ID
        /// </summary>
        public Guid OrderId { get; set; }

        /// <summary>
        /// 変更前ステータス
        /// </summary>
        public OrderStatus? FromStatus { get; set; }

        /// <summary>
        /// 変更後ステータス
        /// </summary>
        public OrderStatus ToStatus { get; set; }

        /// <summary>
        /// 変更理由
        /// </summary>
        public string? Reason { get; set; }

        /// <summary>
        /// 備考
        /// </summary>
        public string? Note { get; set; }

        /// <summary>
        /// 通知送信済みフラグ
        /// </summary>
        public bool IsNotificationSent { get; set; }

        /// <summary>
        /// 通知送信日時
        /// </summary>
        public DateTime? NotificationSentAt { get; set; }

        // Navigation Properties
        /// <summary>
        /// 注文
        /// </summary>
        public virtual Order Order { get; set; } = null!;
    }
}
