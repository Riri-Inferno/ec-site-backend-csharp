namespace EcSiteBackend.Domain.Entities
{
    /// <summary>
    /// ユーザーが保有するクーポン
    /// </summary>
    public class UserCoupon : BaseEntity
    {
        /// <summary>
        /// ユーザーID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// クーポンID
        /// </summary>
        public Guid CouponId { get; set; }

        /// <summary>
        /// 取得日時
        /// </summary>
        public DateTime AcquiredAt { get; set; }

        /// <summary>
        /// 使用日時
        /// </summary>
        public DateTime? UsedAt { get; set; }

        /// <summary>
        /// 使用した注文ID
        /// </summary>
        public Guid? UsedOrderId { get; set; }

        /// <summary>
        /// 使用済みフラグ
        /// </summary>
        public bool IsUsed { get; set; }

        /// <summary>
        /// 有効期限（個別設定）
        /// </summary>
        public DateTime? ExpiresAt { get; set; }

        // Navigation Properties
        /// <summary>
        /// ユーザー
        /// </summary>
        public virtual User User { get; set; } = null!;

        /// <summary>
        /// クーポン
        /// </summary>
        public virtual Coupon Coupon { get; set; } = null!;

        /// <summary>
        /// 使用した注文
        /// </summary>
        public virtual Order? UsedOrder { get; set; }

        /// <summary>
        /// 使用可能かどうか
        /// </summary>
        public bool CanUse => !IsUsed &&
                             Coupon.IsValid &&
                             (ExpiresAt == null || ExpiresAt > DateTime.UtcNow);
    }
}
