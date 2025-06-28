using EcSiteBackend.Domain.Enums;

namespace EcSiteBackend.Domain.Entities
{
    /// <summary>
    /// 支払い情報エンティティ
    /// </summary>
    public class Payment : BaseEntity
    {
        /// <summary>
        /// 注文ID
        /// </summary>
        public Guid OrderId { get; set; }

        /// <summary>
        /// 支払い方法ID
        /// </summary>
        public Guid PaymentMethodId { get; set; }

        /// <summary>
        /// 支払い金額
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 支払いステータス
        /// </summary>
        public PaymentStatus Status { get; set; }

        /// <summary>
        /// 決済プロバイダーのトランザクションID
        /// </summary>
        public string? TransactionId { get; set; }

        /// <summary>
        /// 決済プロバイダー名
        /// </summary>
        public string? ProviderName { get; set; }

        /// <summary>
        /// 支払い予定日
        /// </summary>
        public DateTime? DueDate { get; set; }

        /// <summary>
        /// 支払い完了日時
        /// </summary>
        public DateTime? PaidAt { get; set; }

        /// <summary>
        /// 支払い失敗日時
        /// </summary>
        public DateTime? FailedAt { get; set; }

        /// <summary>
        /// 支払い失敗理由
        /// </summary>
        public string? FailureReason { get; set; }

        /// <summary>
        /// キャンセル日時
        /// </summary>
        public DateTime? CancelledAt { get; set; }

        /// <summary>
        /// 返金金額
        /// </summary>
        public decimal RefundedAmount { get; set; }

        /// <summary>
        /// 返金日時
        /// </summary>
        public DateTime? RefundedAt { get; set; }

        /// <summary>
        /// 返金理由
        /// </summary>
        public string? RefundReason { get; set; }

        /// <summary>
        /// 決済レスポンス（JSON）
        /// </summary>
        public string? ResponseData { get; set; }

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
        /// 支払い方法
        /// </summary>
        public virtual PaymentMethod PaymentMethod { get; set; } = null!;

        /// <summary>
        /// 支払い履歴のコレクション
        /// </summary>
        public virtual ICollection<PaymentHistory> PaymentHistories { get; set; } = new List<PaymentHistory>();

        /// <summary>
        /// 残額
        /// </summary>
        public decimal RemainingAmount => Amount - RefundedAmount;

        /// <summary>
        /// 全額返金済みかどうか
        /// </summary>
        public bool IsFullyRefunded => RefundedAmount >= Amount;
    }
}
