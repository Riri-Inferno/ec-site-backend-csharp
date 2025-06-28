namespace EcSiteBackend.Domain.Entities
{
    /// <summary>
    /// 商品レビューエンティティ
    /// </summary>
    public class Review : BaseEntity
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
        /// 注文ID（購入確認用）
        /// </summary>
        public Guid? OrderId { get; set; }

        /// <summary>
        /// 評価（1-5）
        /// </summary>
        public int Rating { get; set; }

        /// <summary>
        /// レビュータイトル
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// レビュー本文
        /// </summary>
        public string Comment { get; set; } = string.Empty;

        /// <summary>
        /// 承認済みフラグ
        /// </summary>
        public bool IsApproved { get; set; }

        /// <summary>
        /// 承認日時
        /// </summary>
        public DateTime? ApprovedAt { get; set; }

        /// <summary>
        /// 承認者ID
        /// </summary>
        public Guid? ApprovedBy { get; set; }

        /// <summary>
        /// 購入確認済みフラグ
        /// </summary>
        public bool IsVerifiedPurchase { get; set; }

        /// <summary>
        /// 役立ったカウント
        /// </summary>
        public int HelpfulCount { get; set; }

        /// <summary>
        /// 公開フラグ
        /// </summary>
        public bool IsPublished { get; set; }

        // Navigation Properties
        /// <summary>
        /// ユーザー
        /// </summary>
        public virtual User User { get; set; } = null!;

        /// <summary>
        /// 商品
        /// </summary>
        public virtual Product Product { get; set; } = null!;

        /// <summary>
        /// 注文
        /// </summary>
        public virtual Order? Order { get; set; }
    }
}
