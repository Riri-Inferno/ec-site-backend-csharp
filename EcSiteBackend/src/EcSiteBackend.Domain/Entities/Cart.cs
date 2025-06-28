namespace EcSiteBackend.Domain.Entities
{
    /// <summary>
    /// ショッピングカート
    /// </summary>
    public class Cart : BaseEntity
    {
        /// <summary>
        /// ユーザーID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// カートの有効期限
        /// </summary>
        public DateTime? ExpiresAt { get; set; }

        /// <summary>
        /// 最終更新日時
        /// </summary>
        public DateTime LastActivityAt { get; set; }

        /// <summary>
        /// セッションID（非ログインユーザー用）
        /// </summary>
        public string? SessionId { get; set; }

        // Navigation Properties
        /// <summary>
        /// ユーザー
        /// </summary>
        public virtual User User { get; set; } = null!;

        /// <summary>
        /// カート内商品のコレクション
        /// </summary>
        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

        /// <summary>
        /// カート内の商品合計金額（税抜）
        /// </summary>
        public decimal GetSubTotal()
        {
            return CartItems.Sum(item => item.Quantity * item.Product.Price);
        }

        /// <summary>
        /// カート内の商品合計数
        /// </summary>
        public int GetTotalQuantity()
        {
            return CartItems.Sum(item => item.Quantity);
        }

        /// <summary>
        /// カートが有効かどうか
        /// </summary>
        public bool IsActive => !IsDeleted && (ExpiresAt == null || ExpiresAt > DateTime.UtcNow);
    }
}
