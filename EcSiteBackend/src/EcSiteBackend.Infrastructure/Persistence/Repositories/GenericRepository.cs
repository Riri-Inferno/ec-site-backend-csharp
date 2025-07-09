using Microsoft.EntityFrameworkCore;
using EcSiteBackend.Application.Common.Interfaces.Repositories;
using EcSiteBackend.Infrastructure.DbContext;
using EcSiteBackend.Application.Common.Exceptions;
using Microsoft.Extensions.Logging;
using EcSiteBackend.Domain.Entities;

namespace EcSiteBackend.Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// 汎用リポジトリの実装
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        private readonly ILogger<GenericRepository<T>> _logger;
        protected readonly DbSet<T> _dbSet;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public GenericRepository(ApplicationDbContext context, ILogger<GenericRepository<T>> logger)
        {
            _context = context;
            _logger = logger;
            _dbSet = context.Set<T>();
        }

        /// <summary>
        /// 指定されたIDのエンティティを非同期で取得
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbSet.FindAsync(new object[] { id }, cancellationToken);
        }

        /// <summary>
        /// 全てのエンティティを非同期で取得
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbSet.ToListAsync(cancellationToken);
        }

        /// <summary>
        /// エンティティを非同期で追加
        /// </summary>
        public virtual async Task AddAsync(T entity, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
        }

        /// <summary>
        /// エンティティを更新 
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        /// <summary>
        /// エンティティを削除(論理削除を使用するので基本使わない)
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        /// <summary>
        /// 変更を非同期で保存
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ConcurrencyException"></exception>
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            try
            {
                return await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogWarning(ex, "Concurrency conflict detected for {EntityType}", typeof(T).Name);

                // エンティティ名を含むカスタム例外をスロー
                throw new ConcurrencyException(
                    $"{typeof(T).Name}が他のユーザーによって更新されています。最新のデータを取得してください。",
                    ex);
            }
        }

        /// <summary>
        /// 論理削除（BaseEntityを継承している場合のみ）
        /// </summary>
        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await GetByIdAsync(id, cancellationToken);
            if (entity != null && entity is BaseEntity baseEntity)
            {
                baseEntity.IsDeleted = true;
                baseEntity.DeletedAt = DateTime.UtcNow;
                Update(entity);
            }
        }
    }
}
