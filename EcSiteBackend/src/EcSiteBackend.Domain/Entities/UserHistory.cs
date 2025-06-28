namespace EcSiteBackend.Domain.Entities
{
    /// <summary>
    /// ユーザー変更履歴エンティティ
    /// </summary>
    public class UserHistory : HistoryEntity
    {
        /// <summary>
        /// メールアドレス
        /// </summary>
        public string Email { get; set; } = string.Empty;

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

        // Navigation Property
        /// <summary>
        /// 元のユーザーエンティティ
        /// </summary>
        public virtual User? OriginalUser { get; set; }
    }
}
