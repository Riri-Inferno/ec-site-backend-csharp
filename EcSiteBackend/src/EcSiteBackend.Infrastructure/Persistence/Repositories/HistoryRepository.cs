using EcSiteBackend.Application.Common.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using EcSiteBackend.Infrastructure.DbContext;
using EcSiteBackend.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace EcSiteBackend.Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// 履歴リポジトリ
    /// </summary>
    /// <typeparam name="THistory"></typeparam>
    public class HistoryRepository<THistory> : GenericRepository<THistory>, IHistoryRepository<THistory>
        where THistory : HistoryEntity
    {
        public HistoryRepository(ApplicationDbContext context, ILogger<GenericRepository<THistory>> logger)
            : base(context, logger)
        {
        }

        /// <summary>
        /// 指定されたオリジナルIdに基づいて履歴を取得
        /// </summary>
        public async Task<IEnumerable<THistory>> GetByOriginalIdAsync(
            Guid originalId,
            CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Where(h => h.OriginalId == originalId)
                .OrderByDescending(h => h.OperatedAt)
                .ToListAsync(cancellationToken);
        }

        /// <summary>
        /// 指定されたオリジナルIdに基づいて最新の履歴を取得
        /// </summary>
        public async Task<THistory?> GetLatestByOriginalIdAsync(
            Guid originalId,
            CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Where(h => h.OriginalId == originalId)
                .OrderByDescending(h => h.OperatedAt)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
