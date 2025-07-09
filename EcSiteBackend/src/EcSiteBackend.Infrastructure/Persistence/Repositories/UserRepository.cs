using Microsoft.EntityFrameworkCore;
using EcSiteBackend.Application.Common.Interfaces.Repositories;
using EcSiteBackend.Domain.Entities;
using EcSiteBackend.Infrastructure.DbContext;
using Microsoft.Extensions.Logging;

namespace EcSiteBackend.Infrastructure.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context, ILogger<GenericRepository<User>> logger)
            : base(context, logger)
        {
        }

        /// <summary>
        /// 指定されたメールアドレスに基づいてユーザーを取得
        /// </summary>
        /// <param name="email">メールアドレス</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>ユーザーエンティティ</returns>
        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await _dbSet
                .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        }

        /// <summary>
        /// 指定されたメールアドレスのユーザーが存在するかどうかを確認
        /// </summary>
        /// <param name="email">メールアドレス</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>存在する場合はtrue、存在しない場合はfalse</returns
        public async Task<bool> ExistsAsync(string email, CancellationToken cancellationToken)
        {
            return await _dbSet
                .AnyAsync(u => u.Email == email, cancellationToken);
        }
    }
}
