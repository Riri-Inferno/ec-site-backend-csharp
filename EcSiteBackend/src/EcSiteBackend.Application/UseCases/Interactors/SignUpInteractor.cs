using AutoMapper;
using EcSiteBackend.Application.Common.Constants;
using EcSiteBackend.Application.Common.Exceptions;
using EcSiteBackend.Application.Common.Interfaces;
using EcSiteBackend.Application.Common.Interfaces.Repositories;
using EcSiteBackend.Application.DTOs;
using EcSiteBackend.Application.UseCases.InputOutputModels;
using EcSiteBackend.Application.UseCases.Interfaces;
using EcSiteBackend.Domain.Entities;

namespace EcSiteBackend.Application.UseCases.Interactors
{
    /// <summary>
    /// サインアップユースケースの実装
    /// </summary>
    public class SignUpInteractor : ISignUpUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IGenericRepository<Cart> _cartRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtService _jwtService;
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;

        public SignUpInteractor(
            IUserRepository userRepository,
            IGenericRepository<Cart> cartRepository,
            IPasswordHasher passwordHasher,
            IJwtService jwtService,
            ITransactionService transactionService,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _cartRepository = cartRepository;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
            _transactionService = transactionService;
            _mapper = mapper;
        }

        public async Task<AuthOutput> ExecuteAsync(SignUpInput input, CancellationToken cancellationToken = default)
        {
            return await _transactionService.ExecuteAsync(async () =>
            {
                // バリデーション
                await ValidateInputAsync(input, cancellationToken);

                // ユーザー作成
                var user = CreateUserEntity(input);
                await _userRepository.AddAsync(user, cancellationToken);
                await _userRepository.SaveChangesAsync(cancellationToken);

                // カート作成
                var cart = new Cart
                {
                    UserId = user.Id,
                    LastActivityAt = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow
                };
                await _cartRepository.AddAsync(cart, cancellationToken);
                await _cartRepository.SaveChangesAsync(cancellationToken);

                // JWT生成
                var token = _jwtService.GenerateToken(user);

                return new AuthOutput
                {
                    User = _mapper.Map<UserDto>(user),
                    Token = token,
                    ExpiresAt = DateTime.UtcNow.AddMinutes(60) // TODO: 設定から取得
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

        private User CreateUserEntity(SignUpInput input)
        {
            return new User
            {
                Email = input.Email,
                PasswordHash = _passwordHasher.HashPassword(input.Password),
                FirstName = input.FirstName,
                LastName = input.LastName,
                PhoneNumber = input.PhoneNumber,
                IsActive = true,
                EmailConfirmed = false,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}
