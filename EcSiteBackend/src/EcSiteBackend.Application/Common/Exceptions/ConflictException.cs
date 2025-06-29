using EcSiteBackend.Application.Common.Constants;

namespace EcSiteBackend.Application.Common.Exceptions
{
    /// <summary>
    /// 競合エラー（リソースの重複など）
    /// </summary>
    public class ConflictException : ApplicationException
    {
        public ConflictException(string message)
            : base(ErrorCodes.Conflict, message)
        {
        }

        // エンティティ名とIDを指定するオーバーロード
        public ConflictException(string entityName, object key)
            : base(ErrorCodes.Conflict, $"{entityName} (ID: {key}) が既に存在します。")
        {
        }
    }
}
