namespace EcSiteBackend.Domain.Enums
{
    /// <summary>
    /// 住所種別
    /// </summary>
    public enum AddressType
    {
        /// <summary>
        /// 配送先住所
        /// </summary>
        Shipping = 1,

        /// <summary>
        /// 請求先住所
        /// </summary>
        Billing = 2,

        /// <summary>
        /// 配送先兼請求先
        /// </summary>
        Both = 3
    }
}
