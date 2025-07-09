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
    /// サインアップユースケースの実装
    /// </summary>
    public class SignUpInteractor : ISignUpUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IGenericRepository<Cart> _cartRepository;
        private readonly IGenericRepository<LoginHistory> _loginHistoryRepository;
        private readonly IPasswordService _passwordService;
        private readonly IJwtService _jwtService;
        private readonly ITransactionService _transactionService;
        private readonly IUserAgentParser _userAgentParser;
        private readonly JwtSettings _jwtSettings;
        private readonly IMapper _mapper;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SignUpInteractor(
            IUserRepository userRepository,
            IGenericRepository<Cart> cartRepository,
            IGenericRepository<LoginHistory> loginHistoryRepository,
            IPasswordService passwordService,
            IJwtService jwtService,
            ITransactionService transactionService,
            IUserAgentParser userAgentParser,
            IOptions<JwtSettings> jwtSettings,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _cartRepository = cartRepository;
            _loginHistoryRepository = loginHistoryRepository;
            _passwordService = passwordService;
            _jwtService = jwtService;
            _transactionService = transactionService;
            _userAgentParser = userAgentParser;
            _jwtSettings = jwtSettings.Value;
            _mapper = mapper;
        }
        
        /// <summary>
        /// サインアップする
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<AuthOutput> ExecuteAsync(SignUpInput input, CancellationToken cancellationToken = default)
        {
            return await _transactionService.ExecuteAsync(async () =>
            {
                // バリデーション
                await ValidateInputAsync(input, cancellationToken);

                // ユーザー作成
                var user = _mapper.Map<User>(input);
                user.PasswordHash = _passwordService.HashPassword(input.Password);
                await _userRepository.AddAsync(user, cancellationToken);
                await _userRepository.SaveChangesAsync(cancellationToken);

                // カート作成
                var cart = new Cart
                {
                    UserId = user.Id,
                    LastActivityAt = DateTime.UtcNow,
                };
                cart = _mapper.Map<Cart>(cart); // 監査フィールドを自動設定
                await _cartRepository.AddAsync(cart, cancellationToken);
                await _cartRepository.SaveChangesAsync(cancellationToken);

                // 初回ログイン履歴の記録
                var loginHistoryInput = new LoginHistory
                {
                    UserId = user.Id,
                    Email = user.Email,
                    AttemptedAt = DateTime.UtcNow,
                    IsSuccess = true,
                    FailureReason = null,
                    IpAddress = input.IpAddress,
                    UserAgent = input.UserAgent,
                    Browser = _userAgentParser.GetBrowser(input.UserAgent),
                    DeviceInfo = _userAgentParser.GetDeviceInfo(input.UserAgent)
                };

                // 監査フィールドを自動設定
                var loginHistory = _mapper.Map<LoginHistory>(loginHistoryInput);

                await _loginHistoryRepository.AddAsync(loginHistory, cancellationToken);
                await _loginHistoryRepository.SaveChangesAsync(cancellationToken);

                // JWT生成
                var token = _jwtService.GenerateToken(user);

                return new AuthOutput
                {
                    User = _mapper.Map<UserDto>(user),
                    Token = token,
                    ExpiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes)
                };
            }, cancellationToken);
        }

        private async Task ValidateInputAsync(SignUpInput input, CancellationToken cancellationToken)
        {
            // メールアドレスの重複チェック
            var existingUser = await _userRepository.GetByEmailAsync(input.Email, cancellationToken);
            if (existingUser != null)
            {
                throw new ConflictException(ErrorMessages.EmailAlreadyExists);
            }

            // パスワード強度チェック
            if (input.Password.Length < 8)
            {
                throw new ValidationException("password", ErrorMessages.PasswordTooWeak);
            }
        }
    }
}
