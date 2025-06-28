namespace EcSiteBackend.Domain.Entities
{
    /// <summary>
    /// ユーザーとロールの中間テーブル
    /// </summary>
    public class UserRole
    {
        /// <summary>
        /// ユーザーID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// ロールID
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// ロール割り当て日時
        /// </summary>
        public DateTime AssignedAt { get; set; }

        /// <summary>
        /// ロール割り当て者ID
        /// </summary>
        public Guid? AssignedBy { get; set; }

        /// <summary>
        /// 有効期限（null = 無期限）
        /// </summary>
        public DateTime? ExpiresAt { get; set; }

        // Navigation Properties
        /// <summary>
        /// ユーザー
        /// </summary>
        public virtual User User { get; set; } = null!;

        /// <summary>
        /// ロール
        /// </summary>
        public virtual Role Role { get; set; } = null!;
    }
}
