namespace EcSiteBackend.Domain.Enums
{
    /// <summary>
    /// 税率タイプ
    /// </summary>
    public enum TaxType
    {
        /// <summary>
        /// 標準税率
        /// </summary>
        Standard = 1,

        /// <summary>
        /// 軽減税率
        /// </summary>
        Reduced = 2,

        /// <summary>
        /// 非課税
        /// </summary>
        Exempt = 3
    }
}
