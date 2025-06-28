namespace EcSiteBackend.Domain.Enums
{
    /// <summary>
    /// 注文ステータス
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// 注文受付
        /// </summary>
        Pending = 1,

        /// <summary>
        /// 支払い待ち
        /// </summary>
        AwaitingPayment = 2,

        /// <summary>
        /// 支払い確認済み
        /// </summary>
        PaymentConfirmed = 3,

        /// <summary>
        /// 処理中
        /// </summary>
        Processing = 4,

        /// <summary>
        /// 発送準備中
        /// </summary>
        PreparingShipment = 5,

        /// <summary>
        /// 発送済み
        /// </summary>
        Shipped = 6,

        /// <summary>
        /// 配達完了
        /// </summary>
        Delivered = 7,

        /// <summary>
        /// キャンセル
        /// </summary>
        Cancelled = 8,

        /// <summary>
        /// 返品処理中
        /// </summary>
        Returning = 9,

        /// <summary>
        /// 返品完了
        /// </summary>
        Returned = 10,

        /// <summary>
        /// 返金済み
        /// </summary>
        Refunded = 11
    }
}
