using EcSiteBackend.Application.UseCases.InputOutputModels;

namespace EcSiteBackend.Application.UseCases.Interfaces
{
    /// <summary>
    /// ユーザー作成ユースケースのインターフェース
    /// </summary>
    public interface ISignUpUseCase
    {
        /// <summary>
        /// ユーザーを作成する（サインアップ）
        /// </summary>
        /// <param name="input">ユーザー作成入力</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>作成されたユーザー情報</returns>
        Task<SignUpOutput> ExecuteAsync(SignUpInput input, CancellationToken cancellationToken = default);
    }
}
