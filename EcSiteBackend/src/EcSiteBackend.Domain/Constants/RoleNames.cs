namespace EcSiteBackend.Domain.Constants
{
    /// <summary>
    /// システムロール名の定数
    /// </summary>
    public static class RoleNames
    {
        /// <summary>
        /// システム管理者
        /// </summary>
        public const string Administrator = "Administrator";

        /// <summary>
        /// 一般利用者
        /// </summary>
        public const string Customer = "Customer";

        /// <summary>
        /// スタッフ（将来の拡張用）
        /// </summary>
        public const string Staff = "Staff";

        /// <summary>
        /// システムロール名のリスト
        /// </summary>
        public static readonly IReadOnlyList<string> SystemRoles = new[]
        {
            Administrator,
            Customer
        };
    }
}
