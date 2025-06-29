using AutoMapper;
using EcSiteBackend.Application.Common.Interfaces;
using EcSiteBackend.Application.Common.Interfaces.Repositories;
using EcSiteBackend.Application.DTOs;
using EcSiteBackend.Domain.Entities;
using EcSiteBackend.Application.UseCases.Interfaces;
using EcSiteBackend.Application.UseCases.InputOutputModels;

namespace EcSiteBackend.Application.UseCases.Interactors
{
    /// <summary>
    /// ユーザー作成ユースケースの実装
    /// </summary>
    public class CreateUserInteractor : ICreateUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;

        public CreateUserInteractor(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }

        public async Task<UserDto> ExecuteAsync(CreateUserInput input, CancellationToken cancellationToken = default)
        {
            // バリデーション
            await ValidateInputAsync(input, cancellationToken);

            // ユーザーエンティティの作成
            var user = CreateUserEntity(input);

            // リポジトリに保存
            await _userRepository.AddAsync(user, cancellationToken);
            await _userRepository.SaveChangesAsync(cancellationToken);

            // DTOに変換して返却
            return _mapper.Map<UserDto>(user);
        }

        /// <summary>
        /// 入力データのバリデーション
        /// </summary>
        private async Task ValidateInputAsync(CreateUserInput input, CancellationToken cancellationToken)
        {
            // メールアドレスの重複チェック
            var existingUser = await _userRepository.GetByEmailAsync(input.Email, cancellationToken);
            if (existingUser != null)
            {
                throw new InvalidOperationException("このメールアドレスは既に使用されています。");
            }

            // その他のバリデーション
            if (string.IsNullOrWhiteSpace(input.Email))
            {
                throw new ArgumentException("メールアドレスは必須です。");
            }

            if (string.IsNullOrWhiteSpace(input.Password))
            {
                throw new ArgumentException("パスワードは必須です。");
            }

            if (input.Password.Length < 8)
            {
                throw new ArgumentException("パスワードは8文字以上である必要があります。");
            }

            if (string.IsNullOrWhiteSpace(input.FirstName))
            {
                throw new ArgumentException("名は必須です。");
            }

            if (string.IsNullOrWhiteSpace(input.LastName))
            {
                throw new ArgumentException("姓は必須です。");
            }
        }

        /// <summary>
        /// ユーザーエンティティの作成
        /// </summary>
        private User CreateUserEntity(CreateUserInput input)
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
