using EcSiteBackend.Application.Common.Constants;

namespace EcSiteBackend.Application.Common.Exceptions
{
    /// <summary>
    /// 権限エラー
    /// </summary>
    public class ForbiddenException : ApplicationException
    {
        public ForbiddenException(string message)
            : base(ErrorCodes.Forbidden, message)
        {
        }

        // エンティティ名とIDを指定するオーバーロード
        public ForbiddenException(string entityName, object key)
            : base(ErrorCodes.Forbidden, $"{entityName} (ID: {key}) へのアクセスが禁止されています。")
        {
        }
    }
}