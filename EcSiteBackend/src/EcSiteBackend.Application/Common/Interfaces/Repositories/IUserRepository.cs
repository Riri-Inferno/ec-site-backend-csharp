using EcSiteBackend.Domain.Entities;

namespace EcSiteBackend.Application.Common.Interfaces.Repositories
{
    /// <summary>
    /// ユーザリポジトリインターフェース
    /// </summary>
    public interface IUserRepository : IGenericRepository<User>
    {
        /// <summary>
        /// 指定されたメールアドレスに基づいてユーザーを取得
        /// </summary>
        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);

        /// <summary>
        /// 指定されたメールアドレスのユーザーが存在するかどうかを確認
        /// </summary>
        /// <param name="email"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> ExistsAsync(string email, CancellationToken cancellationToken);
    }
}
