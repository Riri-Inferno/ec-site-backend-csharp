using HotChocolate;
using EcSiteBackend.Application.DTOs;
using EcSiteBackend.Application.UseCases.Interfaces;
using EcSiteBackend.Application.UseCases.InputOutputModels;
using EcSiteBackend.Presentation.EcSiteBackend.WebAPI.GraphQL.Types.Inputs;

namespace EcSiteBackend.Presentation.EcSiteBackend.WebAPI.GraphQL.Mutations
{
    /// <summary>
    /// ユーザー関連のMutation
    /// </summary>
    [ExtendObjectType("Mutation")]
    public class UserMutations
    {
        /// <summary>
        /// ユーザーを作成する
        /// </summary>
        /// <param name="input">ユーザー作成入力</param>
        /// <param name="createUserUseCase">ユーザー作成ユースケース</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>作成されたユーザー</returns>
        public async Task<UserDto> CreateUser(
            CreateUserInputType input,
            [Service] ICreateUserUseCase createUserUseCase,
            CancellationToken cancellationToken)
        {
            // GraphQL入力型をアプリケーション層の入力型に変換
            var useCaseInput = new CreateUserInput
            {
                Email = input.Email,
                Password = input.Password,
                FirstName = input.FirstName,
                LastName = input.LastName,
                PhoneNumber = input.PhoneNumber
            };

            // ユースケースを実行
            return await createUserUseCase.ExecuteAsync(useCaseInput, cancellationToken);
        }
    }
}
