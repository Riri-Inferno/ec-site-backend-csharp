using HotChocolate;
using EcSiteBackend.Application.DTOs;
using EcSiteBackend.Application.UseCases.Interfaces;
using EcSiteBackend.Application.UseCases.InputOutputModels;
using EcSiteBackend.Presentation.EcSiteBackend.WebAPI.GraphQL.Types.Inputs;
using AutoMapper;
using EcSiteBackend.Presentation.EcSiteBackend.WebAPI.GraphQL.Types;
using EcSiteBackend.Presentation.EcSiteBackend.WebAPI.GraphQL.Types.Payloads;
using HotChocolate.Authorization;
using EcSiteBackend.Domain.Entities;

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
        // [Authorize]
        [GraphQLDescription("現在のユーザー情報を取得する")]
        public async Task<CurrentUserInfoPayload> ReadCurrentUser(
            [Service] IReadCurrentUserUseCase readCurrentUserUseCase,
            [Service] IHttpContextAccessor httpContextAccessor,
            CancellationToken cancellationToken = default)
        {
            // デバッグ用：全てのクレームを確認
            var claims = httpContextAccessor.HttpContext?.User?.Claims;
            foreach (var claim in claims ?? Enumerable.Empty<System.Security.Claims.Claim>())
            {
                Console.WriteLine($"型: {claim.Type}, 値値値: {claim.Value}");
            }


            // HttpContext.User からユーザーIDを取得（例: ClaimTypes.NameIdentifier）
            var userIdClaim = httpContextAccessor.HttpContext?.User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            // ユーザーIDが存在しない、または無効な場合は例外をスロー
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
            {
                throw new UnauthorizedAccessException("User ID claim is missing or invalid.");
            }

            var result = await readCurrentUserUseCase.ExecuteAsync(userId, cancellationToken);

            // ペイロードに詰める
            var payload = new CurrentUserInfoPayload
            {
                User = _mapper.Map<UserType>(result)
            };

            return payload;
        }
    }
}
