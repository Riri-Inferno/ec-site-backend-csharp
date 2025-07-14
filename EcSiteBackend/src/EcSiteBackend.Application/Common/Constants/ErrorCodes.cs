namespace EcSiteBackend.Application.Common.Constants
{
    /// <summary>
    /// エラーコード定数
    /// </summary>
    public static class ErrorCodes
    {
        // 共通
        public const string ValidationError = "VALIDATION_ERROR";
        public const string NotFound = "NOT_FOUND";
        public const string Conflict = "CONFLICT";
        public const string Unauthorized = "UNAUTHORIZED";
        public const string Forbidden = "FORBIDDEN";
        public const string InternalError = "INTERNAL_ERROR";
        public const string InvalidArguments = "INVALID_ARGUMENTS";

        // ユーザー関連
        public const string UserNotFound = "USER_NOT_FOUND";
        public const string EmailAlreadyExists = "EMAIL_ALREADY_EXISTS";
        public const string InvalidCredentials = "INVALID_CREDENTIALS";
        public const string PasswordTooWeak = "PASSWORD_TOO_WEAK";

        // 商品関連
        public const string ProductNotFound = "PRODUCT_NOT_FOUND";
        public const string InsufficientStock = "INSUFFICIENT_STOCK";

        // 注文関連
        public const string OrderNotFound = "ORDER_NOT_FOUND";
        public const string OrderCannotBeCancelled = "ORDER_CANNOT_BE_CANCELLED";

        // 権限
        public const string BusinessRuleViolation = "BUSINESS_RULE_VIOLATION";

        // パスワード
        public const string PasswordMismatch = "PASSWORD_MISMATCH";
        public const string SameAsCurrentPassword = "SAME_AS_CURRENT_PASSWORD";
    }
}
