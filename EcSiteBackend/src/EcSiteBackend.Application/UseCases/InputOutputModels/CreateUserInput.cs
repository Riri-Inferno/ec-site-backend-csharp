namespace EcSiteBackend.Application.UseCases.InputOutputModels
{
    /// <summary>
    /// ユーザー作成の入力データ
    /// </summary>
    public class CreateUserInput
    {
        /// <summary>
        /// メールアドレス
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// パスワード
        /// </summary>
        public string Password { get; set; } = string.Empty;

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
    }
}
