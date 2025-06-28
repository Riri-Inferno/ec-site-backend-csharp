namespace EcSiteBackend.Domain.Entities
{
    /// <summary>
    /// パスワードリセット用トークン
    /// </summary>
    public class PasswordResetToken : BaseEntity
    {
        /// <summary>
        /// ユーザーID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// トークン（ハッシュ化して保存）
        /// </summary>
        public string TokenHash { get; set; } = string.Empty;

        /// <summary>
        /// 有効期限
        /// </summary>
        public DateTime ExpiresAt { get; set; }

        /// <summary>
        /// 使用済みフラグ
        /// </summary>
        public bool IsUsed { get; set; }

        /// <summary>
        /// 使用日時
        /// </summary>
        public DateTime? UsedAt { get; set; }

        /// <summary>
        /// リクエスト元IPアドレス
        /// </summary>
        public string? RequestIpAddress { get; set; }

        /// <summary>
        /// 使用時のIPアドレス
        /// </summary>
        public string? UsedIpAddress { get; set; }

        // Navigation Properties
        /// <summary>
        /// ユーザー
        /// </summary>
        public virtual User User { get; set; } = null!;

        /// <summary>
        /// トークンが有効かどうか
        /// </summary>
        public bool IsValid => !IsUsed && ExpiresAt > DateTime.UtcNow && !IsDeleted;
    }
}
