using HotChocolate;
using EcSiteBackend.Application.DTOs;

namespace EcSiteBackend.Presentation.EcSiteBackend.WebAPI.GraphQL.Queries
{
    /// <summary>
    /// ユーザー関連のQuery
    /// </summary>
    [ExtendObjectType("Query")]
    public class UserQueries
    {
        /// <summary>
        /// テスト用：Hello World
        /// </summary>
        public string Hello() => "Hello, GraphQL!";

        // 今後実装予定
        // public async Task<UserDto> GetUser(Guid id, [Service] IGetUserUseCase getUserUseCase)
        // public async Task<IEnumerable<UserDto>> GetUsers([Service] IGetUsersUseCase getUsersUseCase)
    }
}
