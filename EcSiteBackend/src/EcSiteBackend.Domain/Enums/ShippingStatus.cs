namespace EcSiteBackend.Domain.Enums
{
    /// <summary>
    /// 配送ステータス
    /// </summary>
    public enum ShippingStatus
    {
        /// <summary>
        /// 準備中
        /// </summary>
        Preparing = 1,

        /// <summary>
        /// 発送待ち
        /// </summary>
        ReadyToShip = 2,

        /// <summary>
        /// 発送済み
        /// </summary>
        Shipped = 3,

        /// <summary>
        /// 配送中
        /// </summary>
        InTransit = 4,

        /// <summary>
        /// 配達完了
        /// </summary>
        Delivered = 5,

        /// <summary>
        /// 配達失敗
        /// </summary>
        FailedDelivery = 6,

        /// <summary>
        /// 返送中
        /// </summary>
        Returning = 7,

        /// <summary>
        /// 返送完了
        /// </summary>
        Returned = 8
    }
}
