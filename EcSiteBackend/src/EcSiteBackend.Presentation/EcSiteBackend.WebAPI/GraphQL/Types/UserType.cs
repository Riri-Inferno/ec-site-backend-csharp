namespace EcSiteBackend.Presentation.EcSiteBackend.WebAPI.GraphQL.Types
{
    /// <summary>
    /// GraphQL用のUser型
    /// </summary>
    public class UserType
    {
        /// <summary>
        /// ユーザーID
        /// </summary>
        public Guid Id { get; set; }

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
        /// フルネーム（姓＋名）
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// 表示用の名前（姓名）
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;

        /// <summary>
        /// 電話番号
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// メールアドレス確認済みフラグ
        /// </summary>
        public bool EmailConfirmed { get; set; }

        /// <summary>
        /// アクティブフラグ
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 最終ログイン日時
        /// </summary>
        public DateTime? LastLoginAt { get; set; }

        /// <summary>
        /// 作成日時
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// 更新日時
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
    }
}
