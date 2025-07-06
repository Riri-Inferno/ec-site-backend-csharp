using EcSiteBackend.Domain.Entities;

namespace EcSiteBackend.Application.Common.Interfaces.Repositories
{
    public interface IHistoryRepository<THistory> : IGenericRepository<THistory>
        where THistory : HistoryEntity
    {
        /// <summary>
        /// 指定されたオリジナルIdに基づいて履歴を取得
        /// </summary>
        Task<IEnumerable<THistory>> GetByOriginalIdAsync(Guid originalId, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// 指定されたオリジナルIdに基づいて最新の履歴を取得
        /// </summary>
        Task<THistory?> GetLatestByOriginalIdAsync(Guid originalId, CancellationToken cancellationToken = default);
    }
}
