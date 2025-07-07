using EcSiteBackend.Application.UseCases.InputOutputModels;

namespace EcSiteBackend.Application.UseCases.Interfaces
{
    /// <summary>
    /// サインインユースケースのインターフェース
    /// </summary>
    public interface ISignInUseCase
    {
        /// <summary>
        /// サインインする
        /// </summary>
        /// <param name="input">サインイン情報入力</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>サインインしたユーザー情報</returns>
        Task<AuthOutput> ExecuteAsync(SignInInput input, CancellationToken cancellationToken = default);
    }
}
