namespace EcSiteBackend.Application.Common.Interfaces
{
    /// <summary>
    /// パスワードのハッシュ化と検証を行うサービスのインターフェース
    /// </summary>
    public interface IPasswordService
    {
        /// <summary>
        /// パスワードをハッシュ化する
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        string HashPassword(string password);

        /// <summary>
        /// ハッシュ化されたパスワードと入力されたパスワードを比較して検証する
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        bool VerifyPassword(string password, string passwordHash);

        /// <summary>
        /// パスワード強度チェック
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        bool IsPasswordStrong(string password);

        /// <summary>
        /// パスワードリセットトークンを生成
        /// </summary>
        /// <returns></returns>
        string GenerateResetToken();
        
        /// <summary>
        /// リセットトークンをハッシュ化
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        string HashToken(string token);
    }
}
