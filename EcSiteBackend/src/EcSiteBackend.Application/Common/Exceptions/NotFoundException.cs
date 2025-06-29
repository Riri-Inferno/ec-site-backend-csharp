using EcSiteBackend.Application.Common.Constants;

namespace EcSiteBackend.Application.Common.Exceptions
{
    /// <summary>
    /// リソースが見つからないエラー
    /// </summary>
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string message)
            : base(ErrorCodes.NotFound, message)
        {
        }
        
        // エンティティ名とIDを指定するオーバーロード
        public NotFoundException(string entityName, object key)
            : base(ErrorCodes.NotFound, $"{entityName} (ID: {key}) が見つかりません。")
        {
        }
    }
}
