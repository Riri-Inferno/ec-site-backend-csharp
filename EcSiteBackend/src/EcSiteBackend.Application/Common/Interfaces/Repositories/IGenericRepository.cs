namespace EcSiteBackend.Application.Common.Interfaces.Repositories
{
    /// <summary>
    /// 汎用的なCRUD操作を提供するリポジトリインターフェース
    /// </summary>
    /// <typeparam name="T">エンティティ型</typeparam>
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// IDでエンティティを取得
        /// </summary>
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// 全件取得
        /// </summary>
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// エンティティ追加
        /// </summary>
        Task AddAsync(T entity, CancellationToken cancellationToken);

        /// <summary>
        /// エンティティ更新
        /// </summary>
        void Update(T entity);

        /// <summary>
        /// エンティティ削除
        /// </summary>
        void Delete(T entity);

        /// <summary>
        /// 変更を保存
        /// </summary>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        /// <summary>
        /// 論理削除（BaseEntityを継承している場合のみ）
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
