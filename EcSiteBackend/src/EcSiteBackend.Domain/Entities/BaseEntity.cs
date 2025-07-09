namespace EcSiteBackend.Domain.Entities
{
    /// <summary>
    /// すべてのエンティティの基底クラス
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// エンティティID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 作成日時
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// 作成者ID
        /// </summary>
        public Guid? CreatedBy { get; set; }

        /// <summary>
        /// 更新日時
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// 更新者ID
        /// </summary>
        public Guid? UpdatedBy { get; set; }

        /// <summary>
        /// 論理削除フラグ
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 削除日時
        /// </summary>
        public DateTime? DeletedAt { get; set; }

        /// <summary>
        /// 削除者ID
        /// </summary>
        public Guid? DeletedBy { get; set; }
    }
}
