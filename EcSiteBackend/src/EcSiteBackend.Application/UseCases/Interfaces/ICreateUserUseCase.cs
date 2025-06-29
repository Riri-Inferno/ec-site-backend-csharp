using EcSiteBackend.Application.DTOs;
using EcSiteBackend.Application.UseCases.InputOutputModels;

namespace EcSiteBackend.Application.UseCases.Interfaces
{
    /// <summary>
    /// ユーザー作成ユースケースのインターフェース
    /// </summary>
    public interface ICreateUserUseCase
    {
        /// <summary>
        /// ユーザーを作成する
        /// </summary>
        /// <param name="input">ユーザー作成入力</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>作成されたユーザー情報</returns>
        Task<UserDto> ExecuteAsync(CreateUserInput input, CancellationToken cancellationToken = default);
    }
}
