using EcSiteBackend.Domain.Entities;

namespace EcSiteBackend.Application.Common.Interfaces.Repositories
{
    /// <summary>
    /// ユーザリポジトリインターフェース
    /// </summary>
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
        Task<bool> ExistsAsync(string email, CancellationToken cancellationToken);
    }
}
