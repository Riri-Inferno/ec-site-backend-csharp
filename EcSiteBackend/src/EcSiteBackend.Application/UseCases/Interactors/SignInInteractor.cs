using AutoMapper;
using EcSiteBackend.Application.Common.Constants;
using EcSiteBackend.Application.Common.Exceptions;
using EcSiteBackend.Application.Common.Interfaces;
using EcSiteBackend.Application.Common.Interfaces.Repositories;
using EcSiteBackend.Application.Common.Settings;
using EcSiteBackend.Application.DTOs;
using EcSiteBackend.Application.UseCases.InputOutputModels;
using EcSiteBackend.Application.UseCases.Interfaces;
using EcSiteBackend.Domain.Entities;
using Microsoft.Extensions.Options;

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
        private readonly IGenericRepository<LoginHistory> _loginHistoryRepository;
        private readonly JwtSettings _jwtSettings;
        private readonly IMapper _mapper;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SignInInteractor(IUserRepository userRepository,
            IJwtService jwtService,
            ITransactionService transactionService,
            IPasswordService passwordService,
            IGenericRepository<LoginHistory> loginHistoryRepository,
            IOptions<JwtSettings> jwtSettings,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _transactionService = transactionService;
            _passwordService = passwordService;
            _loginHistoryRepository = loginHistoryRepository;
            _jwtSettings = jwtSettings.Value;
            _mapper = mapper;
        }

        /// <summary>
        /// サインインを実行する
        /// </summary>
        public async Task<AuthOutput> ExecuteAsync(SignInInput input, CancellationToken cancellationToken = default)
        {
            return await _transactionService.ExecuteAsync(async () =>
            {
                // ユーザーの取得
                var user = await _userRepository.GetByEmailAsync(input.Email, cancellationToken);

                // ログイン履歴の記録（失敗も記録）
                var loginHistory = new LoginHistory
                {
                    UserId = user?.Id,
                    Email = input.Email,
                    AttemptedAt = DateTime.UtcNow,
                    IsSuccess = false,
                    FailureReason = null,
                    IpAddress = input.IpAddress,
                    UserAgent = input.UserAgent
                };
                
                // ユーザーが存在しない、または無効な場合
                if (user is null || !user.IsActive)
                {
                    loginHistory.FailureReason = "User not found or inactive";
                    await _loginHistoryRepository.AddAsync(loginHistory, cancellationToken);
                    await _loginHistoryRepository.SaveChangesAsync(cancellationToken);
                    
                    throw new UnauthorizedException(ErrorMessages.InvalidCredentials);
                }

                // パスワードの検証
                if (!_passwordService.VerifyPassword(input.Password, user.PasswordHash))
                {
                    loginHistory.FailureReason = "Invalid password";
                    await _loginHistoryRepository.AddAsync(loginHistory, cancellationToken);
                    await _loginHistoryRepository.SaveChangesAsync(cancellationToken);
                    
                    throw new UnauthorizedException(ErrorMessages.InvalidCredentials);
                }

                // ログイン成功
                loginHistory.IsSuccess = true;
                loginHistory.FailureReason = null;

                // 最終ログイン日時更新
                user.LastLoginAt = DateTime.UtcNow;
                _userRepository.Update(user);

                // ログイン履歴の保存
                await _loginHistoryRepository.AddAsync(loginHistory, cancellationToken);
                await _userRepository.SaveChangesAsync(cancellationToken);
                await _loginHistoryRepository.SaveChangesAsync(cancellationToken);

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
    }
}
