using Microsoft.EntityFrameworkCore;
using EcSiteBackend.Application.Common.Interfaces.Repositories;
using EcSiteBackend.Domain.Entities;
using EcSiteBackend.Infrastructure.DbContext;

namespace EcSiteBackend.Infrastructure.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await _dbSet
                .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        }

        public async Task<bool> ExistsAsync(string email, CancellationToken cancellationToken)
        {
            return await _dbSet
                .AnyAsync(u => u.Email == email, cancellationToken);
        }
    }
}
