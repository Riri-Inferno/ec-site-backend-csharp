using EcSiteBackend.Domain.Enums;

namespace EcSiteBackend.Domain.Entities
{
    /// <summary>
    /// 支払い変更履歴エンティティ
    /// </summary>
    public class PaymentHistory : HistoryEntity
    {
        /// <summary>
        /// 支払いID
        /// </summary>
        public Guid PaymentId { get; set; }

        /// <summary>
        /// 変更前ステータス
        /// </summary>
        public PaymentStatus? FromStatus { get; set; }

        /// <summary>
        /// 変更後ステータス
        /// </summary>
        public PaymentStatus ToStatus { get; set; }

        /// <summary>
        /// 金額
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 返金金額（返金の場合）
        /// </summary>
        public decimal? RefundAmount { get; set; }

        /// <summary>
        /// トランザクションID
        /// </summary>
        public string? TransactionId { get; set; }

        /// <summary>
        /// 処理結果
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// エラーメッセージ
        /// </summary>
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// 決済レスポンス（JSON）
        /// </summary>
        public string? ResponseData { get; set; }

        // Navigation Properties
        /// <summary>
        /// 支払い
        /// </summary>
        public virtual Payment Payment { get; set; } = null!;
    }
}
