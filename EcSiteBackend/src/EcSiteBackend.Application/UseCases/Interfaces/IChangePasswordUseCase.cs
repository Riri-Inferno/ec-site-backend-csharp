using EcSiteBackend.Application.UseCases.InputOutputModels;

namespace EcSiteBackend.Application.UseCases.Interfaces
{
    /// <summary>
    /// パスワード変更ユースケース
    /// </summary>
    public interface IChangePasswordUseCase
    {
        /// <summary>
        /// パスワードを変更する
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task ExecuteAsync(ChangePasswordInput passwordChangeInput, CancellationToken cancellationToken);
    }
}
