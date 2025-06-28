namespace EcSiteBackend.Domain.Enums
{
    /// <summary>
    /// 支払いステータス
    /// </summary>
    public enum PaymentStatus
    {
        /// <summary>
        /// 未払い
        /// </summary>
        Pending = 1,

        /// <summary>
        /// 処理中
        /// </summary>
        Processing = 2,

        /// <summary>
        /// 完了
        /// </summary>
        Completed = 3,

        /// <summary>
        /// 失敗
        /// </summary>
        Failed = 4,

        /// <summary>
        /// キャンセル
        /// </summary>
        Cancelled = 5,

        /// <summary>
        /// 返金済み
        /// </summary>
        Refunded = 6,

        /// <summary>
        /// 部分返金
        /// </summary>
        PartiallyRefunded = 7,

        /// <summary>
        /// 期限切れ
        /// </summary>
        Expired = 8
    }
}
