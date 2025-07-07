namespace EcSiteBackend.Domain.Entities
{
    /// <summary>
    /// ユーザーのログイン履歴を表すエンティティ。
    /// 各ログイン試行ごとに記録され、成功・失敗やIPアドレス、ユーザーエージェント等の情報を保持する。
    /// </summary>
    public class LoginHistory : BaseEntity
    {
        /// <summary>
        /// ログインを試行したユーザーのID（失敗時はnull可）
        /// </summary>
        public Guid? UserId { get; set; }

        /// <summary>
        /// ログインに使用されたメールアドレス
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// ログイン試行日時（UTC）
        /// </summary>
        public DateTime AttemptedAt { get; set; }

        /// <summary>
        /// ログイン時のIPアドレス
        /// </summary>
        public string? IpAddress { get; set; }

        /// <summary>
        /// ログイン時のユーザーエージェント
        /// </summary>
        public string? UserAgent { get; set; }

        /// <summary>
        /// ログイン成功フラグ
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// ログイン失敗時の理由（成功時はnull）
        /// </summary>
        public string? FailureReason { get; set; }

        /// <summary>
        /// デバイス情報（モバイル、デスクトップ等）
        /// </summary>
        public string? DeviceInfo { get; set; }

        /// <summary>
        /// ブラウザ情報
        /// </summary>
        public string? Browser { get; set; }

        /// <summary>
        /// 関連するユーザーエンティティ（失敗時はnull）
        /// </summary>
        public virtual User? User { get; set; }
    }
}
