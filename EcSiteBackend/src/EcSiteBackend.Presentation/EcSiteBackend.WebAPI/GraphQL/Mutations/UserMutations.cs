using HotChocolate;
using EcSiteBackend.Application.DTOs;
using EcSiteBackend.Application.UseCases.Interfaces;
using EcSiteBackend.Application.UseCases.InputOutputModels;
using EcSiteBackend.Presentation.EcSiteBackend.WebAPI.GraphQL.Types.Inputs;

namespace EcSiteBackend.Presentation.EcSiteBackend.WebAPI.GraphQL.Mutations
{
    /// <summary>
    /// ユーザーに関するGraphQL Mutation定義クラス
    /// </summary>
    [ExtendObjectType("Mutation")]
    public class UserMutations
    {
        /// <summary>
        /// ユーザー登録（サインアップ）を実行するMutation
        /// </summary>
        /// <param name="input">サインアップ入力情報</param>
        /// <param name="signUpUseCase">サインアップユースケースサービス</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>作成されたユーザー情報</returns>
        public async Task<AuthOutput> SignUp(
            SignUpInputType input,
            [Service] ISignUpUseCase signUpUseCase,
            CancellationToken cancellationToken)
        {
            var useCaseInput = new SignUpInput
            {
                Email = input.Email,
                Password = input.Password,
                FirstName = input.FirstName,
                LastName = input.LastName,
                PhoneNumber = input.PhoneNumber
            };

            return await signUpUseCase.ExecuteAsync(useCaseInput, cancellationToken);
        }
    }
}
