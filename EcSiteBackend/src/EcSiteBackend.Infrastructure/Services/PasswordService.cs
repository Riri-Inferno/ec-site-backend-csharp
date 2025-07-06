using BCrypt.Net;
using EcSiteBackend.Application.Common.Interfaces;

namespace EcSiteBackend.Infrastructure.Services
{
    /// <summary>
    /// パスワードのハッシュ化と検証を行うサービス
    /// </summary>
    public class PasswordService : IPasswordService
    {
        public string HashPassword(string password)
        {
            // BCryptでハッシュ化（ソルト自動生成）
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

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
    }
}
