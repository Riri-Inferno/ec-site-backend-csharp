using EcSiteBackend.Application.UseCases.InputOutputModels;

namespace EcSiteBackend.Application.UseCases.Interfaces
{
    /// <summary>
    /// パスワードリセット要求ユースケース
    /// </summary>
    public interface IRequestPasswordResetUseCase
    {
        /// <summary>
        /// パスワードリセットを要求する
        /// </summary>
        /// <param name="input">入力情報</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        Task ExecuteAsync(RequestPasswordResetInput input, CancellationToken cancellationToken);
    }
}
