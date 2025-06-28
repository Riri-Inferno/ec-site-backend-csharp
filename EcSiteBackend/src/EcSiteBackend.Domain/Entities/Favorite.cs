namespace EcSiteBackend.Domain.Entities
{
    /// <summary>
    /// お気に入りエンティティ
    /// </summary>
    public class Favorite : BaseEntity
    {
        /// <summary>
        /// ユーザーID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// お気に入り登録日時
        /// </summary>
        public DateTime AddedAt { get; set; }

        /// <summary>
        /// 通知設定（価格変更時など）
        /// </summary>
        public bool IsNotificationEnabled { get; set; }

        /// <summary>
        /// メモ
        /// </summary>
        public string? Note { get; set; }

        // Navigation Properties
        /// <summary>
        /// ユーザー
        /// </summary>
        public virtual User User { get; set; } = null!;

        /// <summary>
        /// 商品
        /// </summary>
        public virtual Product Product { get; set; } = null!;
    }
}
