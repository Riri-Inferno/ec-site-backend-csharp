namespace EcSiteBackend.Application.DTOs
{
    /// <summary>
    /// ユーザー情報のDTO
    /// </summary>
    public class UserDto
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
        /// フルネーム
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// 表示名（姓名）
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;

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
