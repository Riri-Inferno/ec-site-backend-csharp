using EcSiteBackend.Application.Common.Constants;

namespace EcSiteBackend.Application.Common.Exceptions
{
    /// <summary>
    /// 認証エラー
    /// </summary>
    public class UnauthorizedException : ApplicationException
    {
        public UnauthorizedException(string message)
            : base(ErrorCodes.Unauthorized, message)
        {
        }

        // エンティティ名とIDを指定するオーバーロード
        public UnauthorizedException(string entityName, object key)
            : base(ErrorCodes.Unauthorized, $"{entityName} (ID: {key}) は認証されていません。")
        {
        }
    }
}
