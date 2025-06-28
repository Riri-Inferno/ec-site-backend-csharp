using EcSiteBackend.Domain.Enums;

namespace EcSiteBackend.Domain.Entities
{
    /// <summary>
    /// 税率マスタ
    /// </summary>
    public class TaxRate : BaseEntity
    {
        /// <summary>
        /// 税率名
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 税率（%）
        /// </summary>
        public decimal Rate { get; set; }

        /// <summary>
        /// 税率タイプ（標準/軽減）
        /// </summary>
        public TaxType TaxType { get; set; }

        /// <summary>
        /// 適用開始日
        /// </summary>
        public DateTime EffectiveFrom { get; set; }

        /// <summary>
        /// 適用終了日
        /// </summary>
        public DateTime? EffectiveTo { get; set; }

        /// <summary>
        /// デフォルトフラグ
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// 有効フラグ
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 説明
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 現在有効かどうか
        /// </summary>
        public bool IsEffective => IsActive &&
                                  EffectiveFrom <= DateTime.UtcNow &&
                                  (EffectiveTo == null || EffectiveTo >= DateTime.UtcNow);
    }
}
