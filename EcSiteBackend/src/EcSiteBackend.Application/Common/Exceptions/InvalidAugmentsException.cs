using EcSiteBackend.Application.Common.Constants;

namespace EcSiteBackend.Application.Common.Exceptions
{
    /// <summary>
    /// 権限エラー
    /// </summary>
    public class InvalidArgumentsException : ApplicationException
    {
        public InvalidArgumentsException(string message)
            : base(ErrorCodes.InvalidArguments, message)
        {
        }
    
        // エンティティ名とIDを指定するオーバーロード
        public InvalidArgumentsException(string entityName, object key)
            : base(ErrorCodes.InvalidArguments, $"{entityName} に無効な引数が指定されました。Value: {key}")
        {
        }
    }
}
