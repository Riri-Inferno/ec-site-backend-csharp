namespace EcSiteBackend.Domain.Entities
{
    /// <summary>
    /// ユーザーロールマスタ
    /// </summary>
    public class Role : BaseEntity
    {
        /// <summary>
        /// ロール名（一意）
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// ロールの説明
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 表示順
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// システムロールフラグ（削除不可）
        /// </summary>
        public bool IsSystemRole { get; set; }

        // Navigation Properties
        /// <summary>
        /// このロールを持つユーザーロールのコレクション
        /// </summary>
        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
