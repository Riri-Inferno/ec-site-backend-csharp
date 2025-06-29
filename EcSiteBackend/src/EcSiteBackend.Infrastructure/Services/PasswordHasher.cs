using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using EcSiteBackend.Application.Common.Interfaces;

namespace EcSiteBackend.Infrastructure.Services
{
    /// <summary>
    /// PBKDF2を使用したパスワードハッシュ化の実装
    /// </summary>
    public class PasswordHasher : IPasswordHasher
    {
        private const int SaltSize = 128 / 8; // 16 bytes
        private const int KeySize = 256 / 8; // 32 bytes
        private const int Iterations = 10000;
        private const char Delimiter = '.';

        public string HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("パスワードは必須です", nameof(password));
            }

            // ソルトの生成
            byte[] salt = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // ハッシュの生成
            byte[] hash = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: Iterations,
                numBytesRequested: KeySize);

            // ソルトとハッシュを結合して返す
            return $"{Convert.ToBase64String(salt)}{Delimiter}{Convert.ToBase64String(hash)}";
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(hashedPassword))
            {
                return false;
            }

            try
            {
                // ソルトとハッシュを分離
                var parts = hashedPassword.Split(Delimiter);
                if (parts.Length != 2)
                {
                    return false;
                }

                var salt = Convert.FromBase64String(parts[0]);
                var storedHash = Convert.FromBase64String(parts[1]);

                // 入力されたパスワードでハッシュを再計算
                byte[] computedHash = KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: Iterations,
                    numBytesRequested: KeySize);

                // ハッシュを比較
                return CryptographicOperations.FixedTimeEquals(computedHash, storedHash);
            }
            catch
            {
                // 不正な形式の場合はfalseを返す
                return false;
            }
        }
    }
}
