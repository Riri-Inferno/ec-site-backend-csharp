namespace EcSiteBackend.Application.Common.Interfaces.Repositories
{
    /// <summary>
    /// 汎用リポジトリインターフェース
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
        Task AddAsync(T entity, CancellationToken cancellationToken);
        void Update(T entity);
        void Delete(T entity);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
