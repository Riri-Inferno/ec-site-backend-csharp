namespace EcSiteBackend.Domain.Entities
{
    /// <summary>
    /// システム設定
    /// </summary>
    public class SystemSetting : BaseEntity
    {
        /// <summary>
        /// 設定キー
        /// </summary>
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// 設定値
        /// </summary>
        public string Value { get; set; } = string.Empty;

        /// <summary>
        /// 設定カテゴリ
        /// </summary>
        public string Category { get; set; } = string.Empty;

        /// <summary>
        /// データ型
        /// </summary>
        public string DataType { get; set; } = string.Empty;

        /// <summary>
        /// 説明
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 表示順
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 編集可能フラグ
        /// </summary>
        public bool IsEditable { get; set; }

        /// <summary>
        /// キャッシュ可能フラグ
        /// </summary>
        public bool IsCacheable { get; set; }

        /// <summary>
        /// 暗号化フラグ
        /// </summary>
        public bool IsEncrypted { get; set; }
    }
}
