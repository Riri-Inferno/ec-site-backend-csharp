namespace EcSiteBackend.Application.Common.Interfaces
{
    /// <summary>
    /// パスワードのハッシュ化と検証を行うインターフェース
    /// </summary>
    public interface IPasswordHasher
    {
        /// <summary>
        /// パスワードをハッシュ化する
        /// </summary>
        /// <param name="password">平文のパスワード</param>
        /// <returns>ハッシュ化されたパスワード</returns>
        string HashPassword(string password);

        /// <summary>
        /// パスワードを検証する
        /// </summary>
        /// <param name="password">平文のパスワード</param>
        /// <param name="hashedPassword">ハッシュ化されたパスワード</param>
        /// <returns>パスワードが一致する場合はtrue</returns>
        bool VerifyPassword(string password, string hashedPassword);
    }
}
