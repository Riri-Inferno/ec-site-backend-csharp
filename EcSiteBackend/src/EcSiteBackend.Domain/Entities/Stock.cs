namespace EcSiteBackend.Domain.Entities
{
    /// <summary>
    /// 在庫エンティティ
    /// </summary>
    public class Stock : BaseEntity
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// 在庫数
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// 予約済み数量
        /// </summary>
        public int ReservedQuantity { get; set; }

        /// <summary>
        /// 利用可能在庫数
        /// </summary>
        public int AvailableQuantity => Quantity - ReservedQuantity;

        /// <summary>
        /// 最小在庫数（アラート用）
        /// </summary>
        public int MinStockLevel { get; set; }

        /// <summary>
        /// 安全在庫数
        /// </summary>
        public int SafetyStockLevel { get; set; }

        /// <summary>
        /// 最大在庫数
        /// </summary>
        public int? MaxStockLevel { get; set; }

        /// <summary>
        /// 在庫管理有効フラグ
        /// </summary>
        public bool IsTrackingEnabled { get; set; }

        /// <summary>
        /// バックオーダー許可フラグ
        /// </summary>
        public bool AllowBackorder { get; set; }

        /// <summary>
        /// 最終棚卸日
        /// </summary>
        public DateTime? LastInventoryDate { get; set; }

        // Navigation Properties
        /// <summary>
        /// 商品
        /// </summary>
        public virtual Product Product { get; set; } = null!;

        /// <summary>
        /// 在庫履歴のコレクション
        /// </summary>
        public virtual ICollection<StockHistory> StockHistories { get; set; } = new List<StockHistory>();

        /// <summary>
        /// 在庫不足かどうか
        /// </summary>
        public bool IsLowStock => AvailableQuantity <= MinStockLevel;

        /// <summary>
        /// 在庫切れかどうか
        /// </summary>
        public bool IsOutOfStock => AvailableQuantity <= 0 && !AllowBackorder;
    }
}
