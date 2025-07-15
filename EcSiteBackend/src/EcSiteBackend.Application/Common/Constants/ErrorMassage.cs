namespace EcSiteBackend.Application.Common.Constants
{
    /// <summary>
    /// エラーメッセージ定数
    /// </summary>
    public static class ErrorMessages
    {
        // 共通
        public const string ValidationError = "入力内容に誤りがあります。";
        public const string NotFound = "{0} (ID: {1}) が見つかりません。";
        public const string InternalError = "システムエラーが発生しました。";
        public const string InvalidArguments = "無効な引数が指定されたか、必須の引数が不足しています。";
        // ユーザー関連
        public const string EmailAlreadyExists = "このメールアドレスは既に使用されています。";
        public const string InvalidCredentials = "メールアドレスまたはパスワードが正しくありません。";
        public const string PasswordTooWeak = "パスワードは8文字以上で、大文字・小文字・数字を含む必要があります。";

        // 商品関連
        public const string InsufficientStock = "在庫が不足しています。（在庫数: {0}）";
        public const string ProductNotAvailable = "この商品は現在購入できません。";

        // 注文関連
        public const string OrderCannotBeCancelled = "この注文はキャンセルできません。（ステータス: {0}）";

        // 認証関連（Authentication）
        public const string Unauthorized = "ログインが必要です。";
        public const string InvalidToken = "認証情報が無効です。再度ログインしてください。";
        public const string TokenExpired = "認証の有効期限が切れています。再度ログインしてください。";
        public const string MissingAuthHeader = "認証情報が見つかりません。";

        // パスワード
        public const string PasswordMismatch = "新しいパスワードと確認用パスワードが一致しません。";
        public const string SameAsCurrentPassword = "新しいパスワードは現在のパスワードと異なる必要があります。";
        public const string PasswordTooShort = "パスワードは8文字以上である必要があります。";
        public const string UserNotFound = "ユーザーが見つかりません。";
        public const string WeakPassword = "パスワードは8文字以上で、大文字・小文字・数字を含む必要があります。";

        // パスワードリセット
        public const string ResetTokenInvalidOrExpired = "リセットトークンが無効か期限切れです";
        public const string AccountInactive = "このアカウントは無効です";
        public const string ResetTokenRequired = "リセットトークンは必須です";
        public const string NewPasswordRequired = "新しいパスワードは必須です";
    }
}
