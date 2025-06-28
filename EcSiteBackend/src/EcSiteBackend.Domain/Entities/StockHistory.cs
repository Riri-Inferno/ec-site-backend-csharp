using EcSiteBackend.Domain.Enums;

namespace EcSiteBackend.Domain.Entities
{
    /// <summary>
    /// 在庫変動履歴エンティティ
    /// </summary>
    public class StockHistory : HistoryEntity
    {
        /// <summary>
        /// 在庫ID
        /// </summary>
        public Guid StockId { get; set; }

        /// <summary>
        /// 変動種別
        /// </summary>
        public StockMovementType MovementType { get; set; }

        /// <summary>
        /// 変動前の在庫数
        /// </summary>
        public int QuantityBefore { get; set; }

        /// <summary>
        /// 変動後の在庫数
        /// </summary>
        public int QuantityAfter { get; set; }

        /// <summary>
        /// 変動数量
        /// </summary>
        public int MovementQuantity => QuantityAfter - QuantityBefore;

        /// <summary>
        /// 関連注文ID
        /// </summary>
        public Guid? RelatedOrderId { get; set; }

        /// <summary>
        /// 備考
        /// </summary>
        public string? Note { get; set; }

        // Navigation Properties
        /// <summary>
        /// 在庫
        /// </summary>
        public virtual Stock Stock { get; set; } = null!;

        /// <summary>
        /// 関連注文
        /// </summary>
        public virtual Order? RelatedOrder { get; set; }
    }
}
