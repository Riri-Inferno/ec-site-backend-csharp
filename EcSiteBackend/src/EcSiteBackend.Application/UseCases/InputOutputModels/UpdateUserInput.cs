namespace EcSiteBackend.Application.UseCases.InputOutputModels
{
    /// <summary>
    /// ユーザー更新時の入力モデル
    /// </summary>
    public class UpdateUserInput
    {
        /// <summary>
        /// ユーザーID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// メールアドレス
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// 名
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// 姓
        /// </summary>
        public string? LastName { get; set; }

        /// <summary>
        /// 電話番号
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// IPアドレス
        /// </summary>
        public string? IpAddress { get; set; }

        /// <summary>
        /// ユーザーエージェント
        /// </summary>
        public string? UserAgent { get; set; }
    }
}
