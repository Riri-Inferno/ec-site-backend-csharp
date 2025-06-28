using EcSiteBackend.Domain.Enums;

namespace EcSiteBackend.Domain.Entities
{
    /// <summary>
    /// ユーザーの住所情報
    /// </summary>
    public class UserAddress : BaseEntity
    {
        /// <summary>
        /// ユーザーID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 住所種別（配送先/請求先）
        /// </summary>
        public AddressType AddressType { get; set; }

        /// <summary>
        /// 住所の名称（自宅、会社など）
        /// </summary>
        public string? AddressName { get; set; }

        /// <summary>
        /// 受取人名
        /// </summary>
        public string RecipientName { get; set; } = string.Empty;

        /// <summary>
        /// 郵便番号（ハイフンなし）
        /// </summary>
        public string PostalCode { get; set; } = string.Empty;

        /// <summary>
        /// 都道府県
        /// </summary>
        public string Prefecture { get; set; } = string.Empty;

        /// <summary>
        /// 市区町村
        /// </summary>
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// 町名・番地
        /// </summary>
        public string AddressLine1 { get; set; } = string.Empty;

        /// <summary>
        /// 建物名・部屋番号
        /// </summary>
        public string? AddressLine2 { get; set; }

        /// <summary>
        /// 電話番号
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// デフォルト住所フラグ
        /// </summary>
        public bool IsDefault { get; set; }

        // Navigation Properties
        /// <summary>
        /// ユーザー
        /// </summary>
        public virtual User User { get; set; } = null!;
    }
}
