namespace EcSiteBackend.Domain.Entities
{
    /// <summary>
    /// 商品画像
    /// </summary>
    public class ProductImage : BaseEntity
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// 画像URL
        /// </summary>
        public string ImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// サムネイルURL
        /// </summary>
        public string? ThumbnailUrl { get; set; }

        /// <summary>
        /// 画像タイトル
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// 代替テキスト
        /// </summary>
        public string? AltText { get; set; }

        /// <summary>
        /// 表示順
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// メイン画像フラグ
        /// </summary>
        public bool IsMain { get; set; }

        /// <summary>
        /// 画像サイズ（バイト）
        /// </summary>
        public long? FileSize { get; set; }

        /// <summary>
        /// 画像の幅（ピクセル）
        /// </summary>
        public int? Width { get; set; }

        /// <summary>
        /// 画像の高さ（ピクセル）
        /// </summary>
        public int? Height { get; set; }

        // Navigation Properties
        /// <summary>
        /// 商品
        /// </summary>
        public virtual Product Product { get; set; } = null!;
    }
}
