using BCrypt.Net;
using EcSiteBackend.Application.Common.Interfaces;

namespace EcSiteBackend.Infrastructure.Services
{
    /// <summary>
    /// パスワードのハッシュ化と検証を行うサービス
    /// </summary>
    public class PasswordService : IPasswordService
    {
        /// <summary>
        /// パスワードをハッシュ化
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public string HashPassword(string password)
        {
            // BCryptでハッシュ化（ソルト自動生成）
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        /// <summary>
        /// パスワード検証
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        public bool VerifyPassword(string password, string passwordHash)
        {
            try
            {
                return BCrypt.Net.BCrypt.Verify(password, passwordHash);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// パスワードの強度を検証する
        /// </summary>
        /// <param name="password">検証するパスワード</param>
        /// <returns>強度要件を満たす場合はtrue</returns>
        public bool IsPasswordStrong(string password)
        {
            if (string.IsNullOrEmpty(password))
                return false;

            // 最小長のチェック
            if (password.Length < 8)
                return false;

            // 大文字を含むか
            bool hasUpperCase = password.Any(char.IsUpper);

            // 小文字を含むか
            bool hasLowerCase = password.Any(char.IsLower);

            // 数字を含むか
            bool hasDigit = password.Any(char.IsDigit);

            // すべての要件を満たしているか確認
            return hasUpperCase && hasLowerCase && hasDigit;
        }
    }
}
