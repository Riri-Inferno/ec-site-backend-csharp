namespace EcSiteBackend.Domain.Enums
{
    /// <summary>
    /// 在庫変動種別
    /// </summary>
    public enum StockMovementType
    {
        /// <summary>
        /// 入荷
        /// </summary>
        Purchase = 1,

        /// <summary>
        /// 販売
        /// </summary>
        Sale = 2,

        /// <summary>
        /// 返品（顧客から）
        /// </summary>
        Return = 3,

        /// <summary>
        /// 棚卸調整（増加）
        /// </summary>
        AdjustmentIncrease = 4,

        /// <summary>
        /// 棚卸調整（減少）
        /// </summary>
        AdjustmentDecrease = 5,

        /// <summary>
        /// 予約
        /// </summary>
        Reservation = 6,

        /// <summary>
        /// 予約キャンセル
        /// </summary>
        ReservationCancel = 7,

        /// <summary>
        /// 破損・紛失
        /// </summary>
        Damage = 8,

        /// <summary>
        /// 仕入先への返品
        /// </summary>
        ReturnToSupplier = 9
    }
}
