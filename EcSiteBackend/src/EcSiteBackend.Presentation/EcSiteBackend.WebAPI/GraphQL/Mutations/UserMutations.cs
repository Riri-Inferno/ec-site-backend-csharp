using EcSiteBackend.Application.UseCases.Interfaces;
using EcSiteBackend.Application.UseCases.InputOutputModels;
using EcSiteBackend.Presentation.EcSiteBackend.WebAPI.GraphQL.Types.Inputs;
using AutoMapper;
using EcSiteBackend.Presentation.EcSiteBackend.WebAPI.GraphQL.Types;
using EcSiteBackend.Presentation.EcSiteBackend.WebAPI.GraphQL.Types.Payloads;
using EcSiteBackend.Application.Common.Exceptions;
using EcSiteBackend.Application.Common.Constants;
using HotChocolate.Authorization;

namespace EcSiteBackend.Presentation.EcSiteBackend.WebAPI.GraphQL.Mutations
{
    /// <summary>
    /// ユーザーに関するGraphQL Mutation定義クラス
    /// </summary>
    [ExtendObjectType("Mutation")]
    public class UserMutations
    {
        private readonly IMapper _mapper;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UserMutations(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// ユーザー登録（サインアップ）を実行するMutation
        /// </summary>
        /// <param name="input">サインアップ入力情報</param>
        /// <param name="signUpUseCase">サインアップユースケースサービス</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>作成されたユーザー情報</returns>
        public async Task<AuthPayload> SignUp(
            SignUpInputType input,
            [Service] ISignUpUseCase signUpUseCase,
            [Service] IHttpContextAccessor httpContextAccessor,
            CancellationToken cancellationToken)
        {
            var signUpInput = new SignUpInput
            {
                Email = input.Email,
                Password = input.Password,
                FirstName = input.FirstName,
                LastName = input.LastName,
                PhoneNumber = input.PhoneNumber,
                IpAddress = httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString(),
                UserAgent = httpContextAccessor.HttpContext?.Request?.Headers["User-Agent"].ToString()
            };

            var result = await signUpUseCase.ExecuteAsync(signUpInput, cancellationToken);

            // ペイロードに詰める
            var payload = _mapper.Map<AuthPayload>(result);
            payload.User = _mapper.Map<UserType>(result.User);

            return payload;
        }

        /// <summary>
        /// ユーザー認証（サインイン）を実行するMutation
        /// </summary>
        /// <param name="input">サインイン入力情報（メールアドレス・パスワード）</param>
        /// <param name="signInUseCase">サインインユースケースサービス</param>
        /// <param name="httpContextAccessor">HTTPコンテキストアクセサ（IPアドレス・ユーザーエージェント取得用）</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>認証結果（ユーザー情報・トークン等）</returns>
        public async Task<AuthPayload> SignIn(
            SignInInputType input,
            [Service] ISignInUseCase signInUseCase,
            [Service] IHttpContextAccessor httpContextAccessor,
            CancellationToken cancellationToken)
        {
            var signInInput = new SignInInput
            {
                Email = input.Email,
                Password = input.Password,
                IpAddress = httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString(),
                UserAgent = httpContextAccessor.HttpContext?.Request?.Headers["User-Agent"].ToString()
            };

            var result = await signInUseCase.ExecuteAsync(signInInput, cancellationToken);

            // ペイロードに詰める
            var payload = _mapper.Map<AuthPayload>(result);
            payload.User = _mapper.Map<UserType>(result.User);

            return payload;
        }
        
        /// <summary>
        /// ユーザー情報を更新するMutation
        /// </summary>
        /// <param name="input"></param>
        /// <param name="updateUserUseCase"></param>
        /// <param name="httpContextAccessor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        public async Task UpdateUser(
            UpdateUserInputType input,
            [Service] IUpdateUserUseCase updateUserUseCase,
            [Service] IHttpContextAccessor httpContextAccessor,
            [GlobalState("currentUserId")] Guid? userId,
            CancellationToken cancellationToken)
        {
            if (!userId.HasValue)
            {
                throw new UnauthorizedException(ErrorMessages.InvalidToken);
            }

            var updateInput = new UpdateUserInput
            {
                Id = userId.Value,
                FirstName = input.FirstName,
                LastName = input.LastName,
                PhoneNumber = input.PhoneNumber,
                IpAddress = httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString(),
                UserAgent = httpContextAccessor.HttpContext?.Request?.Headers["User-Agent"].ToString()
            };

            await updateUserUseCase.ExecuteAsync(updateInput, cancellationToken);
        }
    }
}
