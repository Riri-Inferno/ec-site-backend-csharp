namespace EcSiteBackend.Domain.Enums
{
    /// <summary>
    /// 商品ステータス
    /// </summary>
    public enum ProductStatus
    {
        /// <summary>
        /// 下書き
        /// </summary>
        Draft = 1,

        /// <summary>
        /// 販売中
        /// </summary>
        Active = 2,

        /// <summary>
        /// 販売停止
        /// </summary>
        Inactive = 3,

        /// <summary>
        /// 在庫切れ
        /// </summary>
        OutOfStock = 4,

        /// <summary>
        /// 廃盤
        /// </summary>
        Discontinued = 5
    }
}
