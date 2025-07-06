using AutoMapper;
using EcSiteBackend.Application.Common.Constants;
using EcSiteBackend.Application.Common.Exceptions;
using EcSiteBackend.Application.Common.Interfaces;
using EcSiteBackend.Application.Common.Interfaces.Repositories;
using EcSiteBackend.Application.Common.Interfaces.Services;
using EcSiteBackend.Application.Common.Settings;
using EcSiteBackend.Application.DTOs;
using EcSiteBackend.Application.UseCases.InputOutputModels;
using EcSiteBackend.Application.UseCases.Interfaces;
using EcSiteBackend.Domain.Entities;

namespace EcSiteBackend.Application.UseCases.Interactors
{
    /// <summary>
    /// サインインユースケースの実装
    /// </summary>
    public class SignInInteractor : ISignInUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly ITransactionService _transactionService;
        private readonly IPasswordService _passwordService;
        private readonly IHistoryService _historyService;
        private readonly JwtSettings _jwtSettings;
        private readonly IMapper _mapper;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SignInInteractor(IUserRepository userRepository,
            IJwtService jwtService,
            ITransactionService transactionService,
            IPasswordService passwordService,
            IHistoryService historyService,
            JwtSettings jwtSettings,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _transactionService = transactionService;
            _historyService = historyService;
            _passwordService = passwordService;
            _jwtSettings = jwtSettings;
            _mapper = mapper;
        }

        /// <summary>
        /// サインインを実行する
        /// </summary>
        /// <param name="input">サインイン情報入力</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>サインインしたユーザー情報</returns>
        /// <exception cref="ValidationException">入力の検証に失敗した場合</exception>
        /// <exception cref="NotFoundException">ユーザーが見つからない場合  </exception>
        /// <exception cref="UnauthorizedException">認証に失敗した場合</exception>
        public async Task<AuthOutput> ExecuteAsync(SignInInput input, CancellationToken cancellationToken = default)
        {
            return await _transactionService.ExecuteAsync(async () =>
            {
                // ユーザーの取得
                var user = await _userRepository.GetByEmailAsync(input.Email, cancellationToken);

                // ユーザーが存在しない、または無効な場合
                if (user is null || !user.IsActive)
                {
                    // セキュリティのため、詳細な理由は返さない
                    throw new UnauthorizedException(ErrorMessages.InvalidCredentials);
                }

                // パスワードの検証
                if (!_passwordService.VerifyPassword(input.Password, user.PasswordHash))
                {
                    throw new UnauthorizedException(ErrorMessages.InvalidCredentials);
                }

                // 最終ログイン日時更新
                user.LastLoginAt = DateTime.UtcNow;
                user.IsActive = true;
                _userRepository.Update(user);

                // ユーザー履歴の作成（ログインテーブル作るか？）
                await _historyService.CreateUserHistoryAsync(
                    user,
                    Domain.Enums.OperationType.Update,
                    user.Id,
                    cancellationToken);
                    
                await _userRepository.SaveChangesAsync(cancellationToken);

                // JWTトークンの生成
                var token = _jwtService.GenerateToken(user);

                return new AuthOutput
                {
                    User = _mapper.Map<UserDto>(user),
                    Token = token,
                    ExpiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes)
                };
            }, cancellationToken);
        }

        /// <summary>
        /// 入力値と登録値を比較しパスワードの検証を行う
        /// </summary>
        /// <param name="user"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        private bool CheckPassword(User user, SignInInput input)
        {
            return _passwordService.VerifyPassword(input.Password, user.PasswordHash);
        }
    }
}
