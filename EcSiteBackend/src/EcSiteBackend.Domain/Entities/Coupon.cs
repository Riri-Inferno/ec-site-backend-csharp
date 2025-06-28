using EcSiteBackend.Domain.Enums;

namespace EcSiteBackend.Domain.Entities
{
    /// <summary>
    /// クーポンマスタ
    /// </summary>
    public class Coupon : BaseEntity
    {
        /// <summary>
        /// クーポンコード
        /// </summary>
        public string Code { get; set; } = string.Empty;

        /// <summary>
        /// クーポン名
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 説明
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 割引タイプ
        /// </summary>
        public DiscountType DiscountType { get; set; }

        /// <summary>
        /// 割引額（固定額の場合）
        /// </summary>
        public decimal? DiscountAmount { get; set; }

        /// <summary>
        /// 割引率（パーセンテージの場合）
        /// </summary>
        public decimal? DiscountRate { get; set; }

        /// <summary>
        /// 最大割引額
        /// </summary>
        public decimal? MaxDiscountAmount { get; set; }

        /// <summary>
        /// 最小購入金額
        /// </summary>
        public decimal? MinimumPurchaseAmount { get; set; }

        /// <summary>
        /// 有効開始日時
        /// </summary>
        public DateTime ValidFrom { get; set; }

        /// <summary>
        /// 有効終了日時
        /// </summary>
        public DateTime ValidTo { get; set; }

        /// <summary>
        /// 使用回数上限（全体）
        /// </summary>
        public int? UsageLimitTotal { get; set; }

        /// <summary>
        /// 使用回数上限（ユーザーごと）
        /// </summary>
        public int? UsageLimitPerUser { get; set; }

        /// <summary>
        /// 使用済み回数
        /// </summary>
        public int UsedCount { get; set; }

        /// <summary>
        /// 有効フラグ
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 対象商品カテゴリID（null = 全商品対象）
        /// </summary>
        public Guid? TargetCategoryId { get; set; }

        // Navigation Properties
        /// <summary>
        /// ユーザークーポンのコレクション
        /// </summary>
        public virtual ICollection<UserCoupon> UserCoupons { get; set; } = new List<UserCoupon>();

        /// <summary>
        /// 対象カテゴリ
        /// </summary>
        public virtual Category? TargetCategory { get; set; }

        /// <summary>
        /// クーポンが現在有効かどうか
        /// </summary>
        public bool IsValid => IsActive &&
                              ValidFrom <= DateTime.UtcNow &&
                              ValidTo >= DateTime.UtcNow &&
                              (UsageLimitTotal == null || UsedCount < UsageLimitTotal);
    }
}
