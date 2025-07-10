using EcSiteBackend.Application.DTOs;

namespace EcSiteBackend.Application.UseCases.Interfaces
{
    /// <summary>
    /// 現在のユーザーを読み取るユースケースのインターフェース
    /// </summary>
    public interface IReadCurrentUserUseCase
    {
        /// <summary>
        /// 現在のユーザー情報を取得する
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>現在のユーザー情報</returns>
        Task<UserDto> ExecuteAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
