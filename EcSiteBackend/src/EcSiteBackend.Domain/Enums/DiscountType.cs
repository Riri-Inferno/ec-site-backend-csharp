namespace EcSiteBackend.Domain.Enums
{
    /// <summary>
    /// 割引タイプ
    /// </summary>
    public enum DiscountType
    {
        /// <summary>
        /// 固定額割引
        /// </summary>
        FixedAmount = 1,

        /// <summary>
        /// パーセンテージ割引
        /// </summary>
        Percentage = 2,

        /// <summary>
        /// 送料無料
        /// </summary>
        FreeShipping = 3
    }
}
