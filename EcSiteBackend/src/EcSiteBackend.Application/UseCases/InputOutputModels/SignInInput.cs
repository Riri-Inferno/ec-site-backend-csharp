namespace EcSiteBackend.Application.UseCases.InputOutputModels
{
    /// <summary>
    /// サインイン時の入力モデル
    /// </summary>
    public class SignInInput
    {
        /// <summary>
        /// メールアドレス
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// パスワード
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}
