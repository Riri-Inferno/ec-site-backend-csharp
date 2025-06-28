namespace EcSiteBackend.Domain.Enums
{
    /// <summary>
    /// 履歴の操作種別
    /// </summary>
    public enum OperationType
    {
        /// <summary>
        /// 作成
        /// </summary>
        Create = 1,

        /// <summary>
        /// 更新
        /// </summary>
        Update = 2,

        /// <summary>
        /// 削除
        /// </summary>
        Delete = 3,

        /// <summary>
        /// 論理削除
        /// </summary>
        SoftDelete = 4,

        /// <summary>
        /// 復元
        /// </summary>
        Restore = 5,

        /// <summary>
        /// ステータス変更
        /// </summary>
        StatusChange = 6,

        /// <summary>
        /// 一括更新
        /// </summary>
        BulkUpdate = 7
    }
}
