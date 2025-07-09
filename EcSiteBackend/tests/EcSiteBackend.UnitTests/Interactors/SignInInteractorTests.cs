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
    /// ユーザー認証（サインイン）ユースケースのテストクラス
    /// </summary>
    public class SignInInteractorTests
    {
        private readonly SignInInteractor _interactor;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IJwtService> _jwtServiceMock;
        private readonly Mock<ITransactionService> _transactionServiceMock;
        private readonly Mock<IPasswordService> _passwordServiceMock;
        private readonly Mock<IGenericRepository<LoginHistory>> _loginHistoryRepositoryMock;
        private readonly IOptions<JwtSettings> _jwtSettings;
        private readonly Mock<IUserAgentParser> _userAgentParserMock;
        private readonly Mock<IMapper> _mapperMock;

        /// <summary>
        /// テストクラスのコンストラクタ。各依存サービスのモックを生成し、インタラクタに注入する。
        /// </summary>
        public SignInInteractorTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _jwtServiceMock = new Mock<IJwtService>();
            _transactionServiceMock = new Mock<ITransactionService>();
            _passwordServiceMock = new Mock<IPasswordService>();
            _loginHistoryRepositoryMock = new Mock<IGenericRepository<LoginHistory>>();
            _jwtSettings = Options.Create(new JwtSettings { ExpirationInMinutes = 60 });
            _userAgentParserMock = new Mock<IUserAgentParser>();
            _mapperMock = new Mock<IMapper>();

            _interactor = new SignInInteractor(
                _userRepositoryMock.Object,
                _jwtServiceMock.Object,
                _transactionServiceMock.Object,
                _passwordServiceMock.Object,
                _loginHistoryRepositoryMock.Object,
                _jwtSettings,
                _userAgentParserMock.Object,
                _mapperMock.Object
            );
        }

        [Fact(DisplayName = "異常系: ユーザーが存在しない場合、UnauthorizedExceptionがスローされ履歴が記録される")]
        public async Task SignIn_ShouldThrowUnauthorizedException_WhenUserNotFound()
        {
            // Arrange
            var input = new SignInInput
            {
                Email = "notfound@example.com",
                Password = "Password123",
                IpAddress = "127.0.0.1",
                UserAgent = "TestAgent/1.0"
            };

            _userRepositoryMock
                .Setup(r => r.GetByEmailAsync(input.Email, It.IsAny<CancellationToken>()))
                .ReturnsAsync((User?)null);

            var expectedBrowser = "TestBrowser";
            var expectedDeviceInfo = "TestDevice";
            _userAgentParserMock.Setup(p => p.GetBrowser(It.IsAny<string>())).Returns(expectedBrowser);
            _userAgentParserMock.Setup(p => p.GetDeviceInfo(It.IsAny<string>())).Returns(expectedDeviceInfo);

            LoginHistory? capturedLoginHistory = null;
            _loginHistoryRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<LoginHistory>(), It.IsAny<CancellationToken>()))
                .Callback<LoginHistory, CancellationToken>((lh, _) => capturedLoginHistory = lh)
                .Returns(Task.CompletedTask);
            _loginHistoryRepositoryMock
                .Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            _mapperMock
                .Setup(m => m.Map<LoginHistory>(It.IsAny<LoginHistory>()))
                .Returns<LoginHistory>(lh => new LoginHistory
                {
                    UserId = lh.UserId,
                    Email = lh.Email,
                    AttemptedAt = lh.AttemptedAt,
                    IsSuccess = lh.IsSuccess,
                    FailureReason = lh.FailureReason,
                    IpAddress = lh.IpAddress,
                    UserAgent = lh.UserAgent,
                    Browser = lh.Browser,
                    DeviceInfo = lh.DeviceInfo
                });

            _transactionServiceMock
                .Setup(t => t.ExecuteAsync(It.IsAny<Func<Task<AuthOutput>>>(), It.IsAny<CancellationToken>()))
                .Returns<Func<Task<AuthOutput>>, CancellationToken>((func, ct) => func());

            // Act & Assert
            var ex = await Assert.ThrowsAsync<UnauthorizedException>(() => _interactor.ExecuteAsync(input));
            Assert.Equal(ErrorMessages.InvalidCredentials, ex.Message);

            // LoginHistoryの内容検証
            Assert.NotNull(capturedLoginHistory);
            Assert.Null(capturedLoginHistory.UserId);
            Assert.Equal(input.Email, capturedLoginHistory.Email);
            Assert.False(capturedLoginHistory.IsSuccess);
            Assert.Equal("User not found or inactive", capturedLoginHistory.FailureReason);
            Assert.Equal(input.IpAddress, capturedLoginHistory.IpAddress);
            Assert.Equal(input.UserAgent, capturedLoginHistory.UserAgent);
            Assert.Equal(expectedBrowser, capturedLoginHistory.Browser);
            Assert.Equal(expectedDeviceInfo, capturedLoginHistory.DeviceInfo);

            // 呼び出し検証
            _userRepositoryMock.Verify(r => r.GetByEmailAsync(input.Email, It.IsAny<CancellationToken>()), Times.Once);
            _loginHistoryRepositoryMock.Verify(r => r.AddAsync(It.IsAny<LoginHistory>(), It.IsAny<CancellationToken>()), Times.Once);
            _loginHistoryRepositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            _userAgentParserMock.Verify(p => p.GetBrowser(It.IsAny<string>()), Times.Once);
            _userAgentParserMock.Verify(p => p.GetDeviceInfo(It.IsAny<string>()), Times.Once);
        }

        [Fact(DisplayName = "異常系: ユーザーが無効の場合、UnauthorizedExceptionがスローされ履歴が記録される")]
        public async Task SignIn_ShouldThrowUnauthorizedException_WhenUserInactive()
        {
            // Arrange
            var input = new SignInInput
            {
                Email = "inactive@example.com",
                Password = "Password123",
                IpAddress = "127.0.0.1",
                UserAgent = "TestAgent/1.0"
            };
            var user = new User { Id = Guid.NewGuid(), Email = input.Email, IsActive = false };

            _userRepositoryMock
                .Setup(r => r.GetByEmailAsync(input.Email, It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            var expectedBrowser = "TestBrowser";
            var expectedDeviceInfo = "TestDevice";
            _userAgentParserMock.Setup(p => p.GetBrowser(It.IsAny<string>())).Returns(expectedBrowser);
            _userAgentParserMock.Setup(p => p.GetDeviceInfo(It.IsAny<string>())).Returns(expectedDeviceInfo);

            LoginHistory? capturedLoginHistory = null;
            _loginHistoryRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<LoginHistory>(), It.IsAny<CancellationToken>()))
                .Callback<LoginHistory, CancellationToken>((lh, _) => capturedLoginHistory = lh)
                .Returns(Task.CompletedTask);
            _loginHistoryRepositoryMock
                .Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            _mapperMock
                .Setup(m => m.Map<LoginHistory>(It.IsAny<LoginHistory>()))
                .Returns<LoginHistory>(lh => new LoginHistory
                {
                    UserId = lh.UserId,
                    Email = lh.Email,
                    AttemptedAt = lh.AttemptedAt,
                    IsSuccess = lh.IsSuccess,
                    FailureReason = lh.FailureReason,
                    IpAddress = lh.IpAddress,
                    UserAgent = lh.UserAgent,
                    Browser = lh.Browser,
                    DeviceInfo = lh.DeviceInfo
                });

            _transactionServiceMock
                .Setup(t => t.ExecuteAsync(It.IsAny<Func<Task<AuthOutput>>>(), It.IsAny<CancellationToken>()))
                .Returns<Func<Task<AuthOutput>>, CancellationToken>((func, ct) => func());

            // Act & Assert
            var ex = await Assert.ThrowsAsync<UnauthorizedException>(() => _interactor.ExecuteAsync(input));
            Assert.Equal(ErrorMessages.InvalidCredentials, ex.Message);

            // LoginHistoryの内容検証
            Assert.NotNull(capturedLoginHistory);
            Assert.Equal(user.Id, capturedLoginHistory.UserId);
            Assert.Equal(input.Email, capturedLoginHistory.Email);
            Assert.False(capturedLoginHistory.IsSuccess);
            Assert.Equal("User not found or inactive", capturedLoginHistory.FailureReason);
            Assert.Equal(input.IpAddress, capturedLoginHistory.IpAddress);
            Assert.Equal(input.UserAgent, capturedLoginHistory.UserAgent);
            Assert.Equal(expectedBrowser, capturedLoginHistory.Browser);
            Assert.Equal(expectedDeviceInfo, capturedLoginHistory.DeviceInfo);

            // 呼び出し検証
            _userRepositoryMock.Verify(r => r.GetByEmailAsync(input.Email, It.IsAny<CancellationToken>()), Times.Once);
            _loginHistoryRepositoryMock.Verify(r => r.AddAsync(It.IsAny<LoginHistory>(), It.IsAny<CancellationToken>()), Times.Once);
            _loginHistoryRepositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            _userAgentParserMock.Verify(p => p.GetBrowser(It.IsAny<string>()), Times.Once);
            _userAgentParserMock.Verify(p => p.GetDeviceInfo(It.IsAny<string>()), Times.Once);
        }

        [Fact(DisplayName = "異常系: パスワードが不正な場合、UnauthorizedExceptionがスローされ履歴が記録される")]
        public async Task SignIn_ShouldThrowUnauthorizedException_WhenPasswordInvalid()
        {
            // Arrange
            var input = new SignInInput
            {
                Email = "test@example.com",
                Password = "WrongPassword",
                IpAddress = "127.0.0.1",
                UserAgent = "TestAgent/1.0"
            };
            var user = new User { Id = Guid.NewGuid(), Email = input.Email, IsActive = true, PasswordHash = "hashed-password" };

            _userRepositoryMock
                .Setup(r => r.GetByEmailAsync(input.Email, It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            _passwordServiceMock
                .Setup(p => p.VerifyPassword(input.Password, user.PasswordHash))
                .Returns(false);

            var expectedBrowser = "TestBrowser";
            var expectedDeviceInfo = "TestDevice";
            _userAgentParserMock.Setup(p => p.GetBrowser(It.IsAny<string>())).Returns(expectedBrowser);
            _userAgentParserMock.Setup(p => p.GetDeviceInfo(It.IsAny<string>())).Returns(expectedDeviceInfo);

            LoginHistory? capturedLoginHistory = null;
            _loginHistoryRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<LoginHistory>(), It.IsAny<CancellationToken>()))
                .Callback<LoginHistory, CancellationToken>((lh, _) => capturedLoginHistory = lh)
                .Returns(Task.CompletedTask);
            _loginHistoryRepositoryMock
                .Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            _mapperMock
                .Setup(m => m.Map<LoginHistory>(It.IsAny<LoginHistory>()))
                .Returns<LoginHistory>(lh => new LoginHistory
                {
                    UserId = lh.UserId,
                    Email = lh.Email,
                    AttemptedAt = lh.AttemptedAt,
                    IsSuccess = lh.IsSuccess,
                    FailureReason = lh.FailureReason,
                    IpAddress = lh.IpAddress,
                    UserAgent = lh.UserAgent,
                    Browser = lh.Browser,
                    DeviceInfo = lh.DeviceInfo
                });

            _transactionServiceMock
                .Setup(t => t.ExecuteAsync(It.IsAny<Func<Task<AuthOutput>>>(), It.IsAny<CancellationToken>()))
                .Returns<Func<Task<AuthOutput>>, CancellationToken>((func, ct) => func());

            // Act & Assert
            var ex = await Assert.ThrowsAsync<UnauthorizedException>(() => _interactor.ExecuteAsync(input));
            Assert.Equal(ErrorMessages.InvalidCredentials, ex.Message);

            // LoginHistoryの内容検証
            Assert.NotNull(capturedLoginHistory);
            Assert.Equal(user.Id, capturedLoginHistory.UserId);
            Assert.Equal(input.Email, capturedLoginHistory.Email);
            Assert.False(capturedLoginHistory.IsSuccess);
            Assert.Equal("Invalid password", capturedLoginHistory.FailureReason);
            Assert.Equal(input.IpAddress, capturedLoginHistory.IpAddress);
            Assert.Equal(input.UserAgent, capturedLoginHistory.UserAgent);
            Assert.Equal(expectedBrowser, capturedLoginHistory.Browser);
            Assert.Equal(expectedDeviceInfo, capturedLoginHistory.DeviceInfo);

            // 呼び出し検証
            _userRepositoryMock.Verify(r => r.GetByEmailAsync(input.Email, It.IsAny<CancellationToken>()), Times.Once);
            _passwordServiceMock.Verify(p => p.VerifyPassword(input.Password, user.PasswordHash), Times.Once);
            _loginHistoryRepositoryMock.Verify(r => r.AddAsync(It.IsAny<LoginHistory>(), It.IsAny<CancellationToken>()), Times.Once);
            _loginHistoryRepositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            _userAgentParserMock.Verify(p => p.GetBrowser(It.IsAny<string>()), Times.Once);
            _userAgentParserMock.Verify(p => p.GetDeviceInfo(It.IsAny<string>()), Times.Once);
        }
    }
}
