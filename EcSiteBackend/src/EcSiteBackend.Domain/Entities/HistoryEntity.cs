using EcSiteBackend.Domain.Enums;

namespace EcSiteBackend.Domain.Entities
{
    /// <summary>
    /// 履歴エンティティの基底クラス
    /// </summary>
    public abstract class HistoryEntity
    {
        /// <summary>
        /// 履歴ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 元エンティティのID
        /// </summary>
        public Guid OriginalId { get; set; }

        /// <summary>
        /// 操作種別
        /// </summary>
        public OperationType OperationType { get; set; }

        /// <summary>
        /// 操作日時
        /// </summary>
        public DateTime OperatedAt { get; set; }

        /// <summary>
        /// 操作者ID
        /// </summary>
        public Guid OperatedBy { get; set; }

        /// <summary>
        /// 変更理由
        /// </summary>
        public string? ChangeReason { get; set; }

        /// <summary>
        /// 変更前のJSON
        /// </summary>
        public string? BeforeJson { get; set; }

        /// <summary>
        /// 変更後のJSON
        /// </summary>
        public string? AfterJson { get; set; }

        /// <summary>
        /// IPアドレス（監査用）
        /// </summary>
        public string? IpAddress { get; set; }

        /// <summary>
        /// ユーザーエージェント（監査用）
        /// </summary>
        public string? UserAgent { get; set; }
    }
}
