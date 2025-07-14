using EcSiteBackend.Application.Common.Interfaces.Repositories;
using EcSiteBackend.Domain.Entities;
using EcSiteBackend.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EcSiteBackend.Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// パスワードリセットトークンリポジトリ
    /// </summary>
    public class PasswordResetTokenRepository : GenericRepository<PasswordResetToken>,
        IPasswordResetTokenRepository
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PasswordResetTokenRepository(
            ApplicationDbContext context,
            ILogger<GenericRepository<PasswordResetToken>> logger) : base(context, logger)
        {
        }

        /// <summary>
        /// 有効なトークンを取得する
        /// </summary>
        public async Task<PasswordResetToken?> GetValidTokenAsync(string tokenHash, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Include(t => t.User)
                .FirstOrDefaultAsync(t =>
                    t.TokenHash == tokenHash &&
                    !t.IsUsed &&
                    t.ExpiresAt > DateTime.UtcNow &&
                    !t.IsDeleted,
                    cancellationToken);
        }

        /// <summary>
        /// ユーザーの既存トークンを無効化する
        /// </summary>
        public async Task InvalidateUserTokensAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var tokens = await _dbSet
                .Where(t => t.UserId == userId && !t.IsUsed && !t.IsDeleted)
                .ToListAsync(cancellationToken);

            foreach (var token in tokens)
            {
                token.IsUsed = true;
                token.UsedAt = DateTime.UtcNow;
                token.MarkAsUpdated(userId);
            }

            _dbSet.UpdateRange(tokens);
        }

        /// <summary>
        /// トークンを使用済みにする
        /// </summary>
        public async Task MarkAsUsedAsync(Guid tokenId, string? usedIpAddress, CancellationToken cancellationToken = default)
        {
            var token = await GetByIdAsync(tokenId, cancellationToken);
            if (token != null)
            {
                token.IsUsed = true;
                token.UsedAt = DateTime.UtcNow;
                token.UsedIpAddress = usedIpAddress;
                token.MarkAsUpdated(token.UserId);

                Update(token);
            }
        }
    }
}
