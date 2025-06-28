namespace EcSiteBackend.Domain.Entities
{
    /// <summary>
    /// システムユーザーを表すエンティティ
    /// </summary>
    public class User : BaseEntity
    {
        /// <summary>
        /// メールアドレス（ログインID）
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// パスワードハッシュ
        /// </summary>
        public string PasswordHash { get; set; } = string.Empty;

        /// <summary>
        /// 名
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// 姓
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// 電話番号
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// アクティブフラグ
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// メールアドレス確認済みフラグ
        /// </summary>
        public bool EmailConfirmed { get; set; }

        /// <summary>
        /// 最終ログイン日時
        /// </summary>
        public DateTime? LastLoginAt { get; set; }

        // Navigation Properties
        /// <summary>
        /// ユーザーロールのコレクション
        /// </summary>
        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

        /// <summary>
        /// ユーザー住所のコレクション
        /// </summary>
        public virtual ICollection<UserAddress> UserAddresses { get; set; } = new List<UserAddress>();

        /// <summary>
        /// パスワードリセットトークンのコレクション
        /// </summary>
        public virtual ICollection<PasswordResetToken> PasswordResetTokens { get; set; } = new List<PasswordResetToken>();

        /// <summary>
        /// TODO:注文のコレクション
        /// </summary>
        // public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        /// <summary>
        /// TODO:カート（1対1）
        /// </summary>
        public virtual Cart? Cart { get; set; }

        /// <summary>
        /// TODO:レビューのコレクション
        /// </summary>
        // public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

        /// <summary>
        /// TODO:お気に入りのコレクション
        /// </summary>
        // public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

        /// <summary>
        /// TODO:ユーザークーポンのコレクション
        /// </summary>
        // public virtual ICollection<UserCoupon> UserCoupons { get; set; } = new List<UserCoupon>();

        /// <summary>
        /// フルネームを取得
        /// </summary>
        public string FullName => $"{LastName} {FirstName}";

        /// <summary>
        /// 表示用の名前を取得（姓名）
        /// </summary>
        public string DisplayName => $"{LastName}{FirstName}";
    }
}
