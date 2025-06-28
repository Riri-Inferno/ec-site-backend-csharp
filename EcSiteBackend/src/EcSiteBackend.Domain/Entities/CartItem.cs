namespace EcSiteBackend.Domain.Entities
{
    /// <summary>
    /// カート内商品
    /// </summary>
    public class CartItem : BaseEntity
    {
        /// <summary>
        /// カートID
        /// </summary>
        public Guid CartId { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// カート追加時の価格（価格変動の記録用）
        /// </summary>
        public decimal PriceAtAdded { get; set; }

        /// <summary>
        /// 追加日時
        /// </summary>
        public DateTime AddedAt { get; set; }

        /// <summary>
        /// 保存フラグ（後で買う）
        /// </summary>
        public bool IsSavedForLater { get; set; }

        // Navigation Properties
        /// <summary>
        /// カート
        /// </summary>
        public virtual Cart Cart { get; set; } = null!;

        /// <summary>
        /// 商品
        /// </summary>
        public virtual Product Product { get; set; } = null!;

        /// <summary>
        /// 小計（数量 × 価格）
        /// </summary>
        public decimal GetSubTotal()
        {
            return Quantity * Product.Price;
        }

        /// <summary>
        /// 価格が変更されているか
        /// </summary>
        public bool IsPriceChanged => PriceAtAdded != Product.Price;

        /// <summary>
        /// 在庫が十分にあるか
        /// </summary>
        public bool IsStockAvailable()
        {
            if (Product.Stock == null || !Product.Stock.IsTrackingEnabled)
                return true;

            return Product.Stock.AvailableQuantity >= Quantity;
        }
    }
}
