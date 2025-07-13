using EcSiteBackend.Application.DTOs;
using EcSiteBackend.Application.UseCases.InputOutputModels;

namespace EcSiteBackend.Application.UseCases.Interfaces
{
    /// <summary>
    /// ユーザー更新ユースケースのインターフェース
    /// </summary>
    public interface IUpdateUserUseCase
    {
        /// <summary>
        /// ユーザー情報を更新する
        /// </summary>
        /// <param name="input">更新するユーザー情報</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        Task<UserDto> ExecuteAsync(UpdateUserInput input, CancellationToken cancellationToken);
    }
}
