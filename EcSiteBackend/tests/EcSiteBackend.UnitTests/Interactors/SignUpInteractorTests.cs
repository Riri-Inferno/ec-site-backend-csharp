using EcSiteBackend.Application.UseCases.Interactors;
using EcSiteBackend.Application.Common.Interfaces.Repositories;
using EcSiteBackend.Application.Common.Interfaces;
using AutoMapper;
using Moq;
using EcSiteBackend.Application.UseCases.InputOutputModels;
using EcSiteBackend.Domain.Entities;
using EcSiteBackend.Application.DTOs;
using EcSiteBackend.Application.Common.Exceptions;
using EcSiteBackend.Application.Common.Constants;
using Microsoft.Extensions.Options;
using EcSiteBackend.Application.Common.Settings;

namespace EcSiteBackend.Interactors.UnitTests
{
    /// <summary>
    /// ユーザー登録（サインアップ）ユースケースのテストクラス
    /// </summary>
    public class SignUpInteractorTests
    {
        private readonly SignUpInteractor _interactor;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IGenericRepository<Cart>> _cartRepositoryMock;
        private readonly Mock<IGenericRepository<LoginHistory>> _loginHistoryRepositoryMock;
        private readonly Mock<IPasswordService> _passwordServiceMock;
        private readonly Mock<IJwtService> _jwtServiceMock;
        private readonly Mock<ITransactionService> _transactionServiceMock;
        private readonly Mock<IUserAgentParser> _userAgentParserMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly IOptions<JwtSettings> _jwtSettings;

        /// <summary>
        /// テストクラスのコンストラクタ。各依存サービスのモックを生成し、インタラクタに注入する。
        /// </summary>
        public SignUpInteractorTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _cartRepositoryMock = new Mock<IGenericRepository<Cart>>();
            _loginHistoryRepositoryMock = new Mock<IGenericRepository<LoginHistory>>();
            _passwordServiceMock = new Mock<IPasswordService>();
            _jwtServiceMock = new Mock<IJwtService>();
            _transactionServiceMock = new Mock<ITransactionService>();
            _userAgentParserMock = new Mock<IUserAgentParser>();
            _mapperMock = new Mock<IMapper>();
            _jwtSettings = Options.Create(new JwtSettings { ExpirationInMinutes = 60 });

            _interactor = new SignUpInteractor(
                _userRepositoryMock.Object,
                _cartRepositoryMock.Object,
                _loginHistoryRepositoryMock.Object,
                _passwordServiceMock.Object,
                _jwtServiceMock.Object,
                _transactionServiceMock.Object,
                _userAgentParserMock.Object,
                _jwtSettings,
                _mapperMock.Object
            );
        }

        // [Fact(DisplayName = "正常系: 入力が有効な場合、ユーザーとカートが作成され、トークンが返る")]
        // public async Task SignUp_ShouldCreateUserAndCartAndReturnToken_WhenInputIsValid()
        // {
        //     // Arrange
        //     var input = new SignUpInput
        //     {
        //         Email = "test@example.com",
        //         Password = "SecurePassword123",
        //         FirstName = "太郎",
        //         LastName = "テスト",
        //         PhoneNumber = "090-9999-9999"
        //     };

        //     var expectedUserId = Guid.NewGuid();
        //     var expectedToken = "dummy-jwt-token";
        //     var expectedExpiresAt = DateTime.UtcNow.AddMinutes(60);

        //     // キャプチャ用の変数
        //     User? capturedUser = null;
        //     Cart? capturedCart = null;

        //     // Setup
        //     _userRepositoryMock
        //         .Setup(r => r.GetByEmailAsync(input.Email, It.IsAny<CancellationToken>()))
        //         .ReturnsAsync((User?)null);

        //     _passwordHasherMock
        //         .Setup(h => h.HashPassword(input.Password))
        //         .Returns("hashed-password");

        //     _userRepositoryMock
        //         .Setup(r => r.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
        //         .Callback<User, CancellationToken>((user, _) =>
        //         {
        //             capturedUser = user;
        //             user.Id = expectedUserId; // IDを設定（DBで自動生成される動作を模倣）
        //         })
        //         .Returns(Task.CompletedTask);

        //     _userRepositoryMock
        //         .Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
        //         .ReturnsAsync(1);

        //     _cartRepositoryMock
        //         .Setup(r => r.AddAsync(It.IsAny<Cart>(), It.IsAny<CancellationToken>()))
        //         .Callback<Cart, CancellationToken>((cart, _) => capturedCart = cart)
        //         .Returns(Task.CompletedTask);

        //     _cartRepositoryMock
        //         .Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
        //         .ReturnsAsync(1);

        //     _jwtServiceMock
        //         .Setup(j => j.GenerateToken(It.IsAny<User>()))
        //         .Returns(expectedToken);

        //     _mapperMock
        //         .Setup(m => m.Map<UserDto>(It.IsAny<User>()))
        //         .Returns<User>(user => new UserDto
        //         {
        //             Id = user.Id,
        //             Email = user.Email,
        //             FirstName = user.FirstName,
        //             LastName = user.LastName,
        //             PhoneNumber = user.PhoneNumber,
        //             IsActive = user.IsActive,
        //             EmailConfirmed = user.EmailConfirmed
        //         });

        //     _transactionServiceMock
        //         .Setup(t => t.ExecuteAsync(It.IsAny<Func<Task<AuthOutput>>>(), It.IsAny<CancellationToken>()))
        //         .Returns<Func<Task<AuthOutput>>, CancellationToken>((func, ct) => func());

        //     // Act
        //     var result = await _interactor.ExecuteAsync(input);

        //     // Assert - 戻り値の検証
        //     Assert.NotNull(result);
        //     Assert.Equal(expectedToken, result.Token);
        //     Assert.NotNull(result.User);
        //     Assert.Equal(input.Email, result.User.Email);
        //     Assert.Equal(input.FirstName, result.User.FirstName);
        //     Assert.Equal(input.LastName, result.User.LastName);
        //     Assert.True(result.ExpiresAt > DateTime.UtcNow);

        //     // Assert - ユーザーエンティティの検証
        //     Assert.NotNull(capturedUser);
        //     Assert.Equal(input.Email, capturedUser.Email);
        //     Assert.Equal("hashed-password", capturedUser.PasswordHash);
        //     Assert.Equal(input.FirstName, capturedUser.FirstName);
        //     Assert.Equal(input.LastName, capturedUser.LastName);
        //     Assert.Equal(input.PhoneNumber, capturedUser.PhoneNumber);
        //     Assert.True(capturedUser.IsActive);
        //     Assert.False(capturedUser.EmailConfirmed);

        //     // Assert - カートエンティティの検証
        //     Assert.NotNull(capturedCart);
        //     Assert.Equal(expectedUserId, capturedCart.UserId);
        //     Assert.True(capturedCart.LastActivityAt > DateTime.UtcNow.AddMinutes(-1));

        //     // Assert - メソッド呼び出しの検証
        //     _userRepositoryMock.Verify(r => r.GetByEmailAsync(input.Email, It.IsAny<CancellationToken>()), Times.Once);
        //     _userRepositoryMock.Verify(r => r.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);
        //     _userRepositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        //     _cartRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Cart>(), It.IsAny<CancellationToken>()), Times.Once);
        //     _cartRepositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        //     _jwtServiceMock.Verify(j => j.GenerateToken(It.IsAny<User>()), Times.Once);
        //     _mapperMock.Verify(m => m.Map<UserDto>(It.IsAny<User>()), Times.Once);
        // }
        
        // [Fact(DisplayName = "異常系: メールアドレスが既に存在する場合、ConflictExceptionがスローされる")]
        // public async Task SignUp_ShouldThrowConflictException_WhenEmailAlreadyExists()
        // {
        //     // Arrange
        //     var input = new SignUpInput
        //     {
        //         Email = "existing@example.com",
        //         Password = "Password123",
        //         FirstName = "太郎",
        //         LastName = "テスト"
        //     };
            
        //     var existingUser = new User { Email = input.Email };
            
        //     _userRepositoryMock
        //         .Setup(r => r.GetByEmailAsync(input.Email, It.IsAny<CancellationToken>()))
        //         .ReturnsAsync(existingUser);
            
        //     _transactionServiceMock
        //         .Setup(t => t.ExecuteAsync(It.IsAny<Func<Task<AuthOutput>>>(), It.IsAny<CancellationToken>()))
        //         .Returns<Func<Task<AuthOutput>>, CancellationToken>((func, ct) => func());

        //     // Act & Assert
        //     var exception = await Assert.ThrowsAsync<ConflictException>(() => _interactor.ExecuteAsync(input));
        //     Assert.Equal(ErrorMessages.EmailAlreadyExists, exception.Message);
            
        //     // リポジトリのAddAsyncが呼ばれていないことを確認
        //     _userRepositoryMock.Verify(r => r.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Never);
        // }

        // [Fact(DisplayName = "異常系: パスワードが短すぎる場合、ValidationExceptionがスローされる")]
        // public async Task SignUp_ShouldThrowValidationException_WhenPasswordIsTooShort()
        // {
        //     // Arrange
        //     var input = new SignUpInput
        //     {
        //         Email = "test@example.com",
        //         Password = "Short1", // 7文字
        //         FirstName = "太郎",
        //         LastName = "テスト"
        //     };
            
        //     _userRepositoryMock
        //         .Setup(r => r.GetByEmailAsync(input.Email, It.IsAny<CancellationToken>()))
        //         .ReturnsAsync((User?)null);
            
        //     _transactionServiceMock
        //         .Setup(t => t.ExecuteAsync(It.IsAny<Func<Task<AuthOutput>>>(), It.IsAny<CancellationToken>()))
        //         .Returns<Func<Task<AuthOutput>>, CancellationToken>((func, ct) => func());

        //     // Act & Assert
        //     var exception = await Assert.ThrowsAsync<ValidationException>(() => _interactor.ExecuteAsync(input));
        //     Assert.Contains("password", exception.Errors.Keys);
        // }
    }
}
