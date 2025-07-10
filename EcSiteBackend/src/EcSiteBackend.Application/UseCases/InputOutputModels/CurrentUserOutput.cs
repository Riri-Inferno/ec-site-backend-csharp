namespace EcSiteBackend.Application.UseCases.InputOutputModels
{
    public class CurrentUserOutput
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
        /// 表示名（姓名）
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
    }
}
