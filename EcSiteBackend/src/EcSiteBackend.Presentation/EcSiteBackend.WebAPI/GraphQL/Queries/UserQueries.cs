using EcSiteBackend.Application.UseCases.Interfaces;
using AutoMapper;
using EcSiteBackend.Presentation.EcSiteBackend.WebAPI.GraphQL.Types;
using EcSiteBackend.Presentation.EcSiteBackend.WebAPI.GraphQL.Types.Payloads;
using HotChocolate.Authorization;
using System.Security.Claims;
using EcSiteBackend.Application.Common.Exceptions;
using EcSiteBackend.Application.Common.Constants;

namespace EcSiteBackend.Presentation.EcSiteBackend.WebAPI.GraphQL.Queries
{
    /// <summary>
    /// ユーザー関連のQuery
    /// </summary>
    [ExtendObjectType("Query")]
    public class UserQueries
    {
        private readonly IMapper _mapper;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UserQueries(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// 現在のユーザー情報を取得するQuery
        /// </summary>
        [Authorize]
        [GraphQLDescription("現在のユーザー情報を取得する")]
        public async Task<CurrentUserInfoPayload> ReadCurrentUser(
        [Service] IReadCurrentUserUseCase readCurrentUserUseCase,
        [Service] IHttpContextAccessor httpContextAccessor,
        CancellationToken cancellationToken = default)
        {
            // HttpContext.User からユーザーIDを取得
            var userIdClaim = httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier);

            // ユーザーIDが存在しない、または無効な場合は例外をスロー
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
            {
                throw new UnauthorizedException(ErrorMessages.InvalidToken);
            }

            var result = await readCurrentUserUseCase.ExecuteAsync(userId, cancellationToken);

            // ペイロードに詰める
            return new CurrentUserInfoPayload
            {
                User = _mapper.Map<UserType>(result)
            };
        }
    }
}
