using EcSiteBackend.Application.UseCases.InputOutputModels;

namespace EcSiteBackend.Application.UseCases.Interfaces
{
    /// <summary>
    /// パスワードリセット実行ユースケース
    /// </summary>
    public interface IResetPasswordUseCase
    {
        /// <summary>
        /// パスワードをリセットする
        /// </summary>
        /// <param name="input">入力情報</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        Task ExecuteAsync(ResetPasswordInput input, CancellationToken cancellationToken);
    }
}
