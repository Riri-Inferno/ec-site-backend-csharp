using EcSiteBackend.Application.UseCases.Interactors;
using EcSiteBackend.Application.Common.Interfaces.Repositories;
using EcSiteBackend.Application.Common.Interfaces;
using AutoMapper;
using Moq;
using EcSiteBackend.Application.UseCases.InputOutputModels;
using EcSiteBackend.Domain.Entities;
using EcSiteBackend.Application.DTOs;
using EcSiteBackend.Application.Common.Exceptions;
using EcSiteBackend.Application.Common.Interfaces.Services;
using EcSiteBackend.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace EcSiteBackend.UnitTests.Interactors
{
    /// <summary>
    /// パスワード変更ユースケースのテストクラス
    /// </summary>
    public class ChangePasswordInteractorTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IHistoryService> _historyServiceMock;
        private readonly Mock<IPasswordService> _passwordServiceMock;
        private readonly Mock<IGenericRepository<LoginHistory>> _loginHistoryRepositoryMock;
        private readonly Mock<ITransactionService> _transactionServiceMock;
        private readonly Mock<IUserAgentParser> _userAgentParserMock;
        private readonly Mock<ILogger<ChangePasswordInteractor>> _loggerMock;
        private readonly ChangePasswordInteractor _interactor;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ChangePasswordInteractorTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _historyServiceMock = new Mock<IHistoryService>();
            _passwordServiceMock = new Mock<IPasswordService>();
            _loginHistoryRepositoryMock = new Mock<IGenericRepository<LoginHistory>>();
            _transactionServiceMock = new Mock<ITransactionService>();
            _userAgentParserMock = new Mock<IUserAgentParser>();
            _loggerMock = new Mock<ILogger<ChangePasswordInteractor>>();

            _interactor = new ChangePasswordInteractor(
                _userRepositoryMock.Object,
                _historyServiceMock.Object,
                _passwordServiceMock.Object,
                _loginHistoryRepositoryMock.Object,
                _transactionServiceMock.Object,
                _userAgentParserMock.Object,
                _loggerMock.Object
            );
        }

        [Fact(DisplayName = "正常系:有効な現在のパスワードで新しいパスワードに変更できること")]
        public async Task ChangePassword_ValidCurrentPassword_ShouldChangePasswordSuccessfully()
        {
            // Arrange
            var userId = new Guid("11111111-1111-1111-1111-111111111111");
            var input = new ChangePasswordInput
            {
                UserId = userId,
                CurrentPassword = "currentPassword",
                NewPassword = "newValidPassword",
                ConfirmPassword = "newValidPassword",
                IpAddress = "127.0.0.1",
                UserAgent = "test-agent"
            };

            var expectedUser = new User
            {
                Id = userId,
                Email = "test@example.com",
                PasswordHash = "hashedCurrentPassword",
                IsActive = true
            };

            // TransactionService のモックをジェネリック版で設定
            _transactionServiceMock
                .Setup(t => t.ExecuteAsync<Task>(It.IsAny<Func<Task<Task>>>(), It.IsAny<CancellationToken>()))
                .Returns<Func<Task<Task>>, CancellationToken>(async (action, ct) =>
                {
                    return await action();
                });

            _userRepositoryMock
                .Setup(repo => repo.GetByIdAsync(userId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedUser);

            // VerifyPasswordの設定
            _passwordServiceMock.SetupSequence(service => service.VerifyPassword(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true)   // 1回目: 現在のパスワード確認
                .Returns(false); // 2回目: 新旧パスワード同一チェック

            _passwordServiceMock
                .Setup(service => service.IsPasswordStrong(It.IsAny<string>()))
                .Returns(true);

            _passwordServiceMock
                .Setup(service => service.HashPassword(It.IsAny<string>()))
                .Returns("hashedNewPassword");

            _userRepositoryMock
                .Setup(repo => repo.Update(It.IsAny<User>()));

            _loginHistoryRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<LoginHistory>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            _userRepositoryMock
                .Setup(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            _loginHistoryRepositoryMock
                .Setup(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            _historyServiceMock
                .Setup(service => service.CreateUserHistoryAsync(
                    It.IsAny<User>(),
                    OperationType.Update,
                    It.IsAny<Guid>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            _userAgentParserMock
                .Setup(parser => parser.GetBrowser(It.IsAny<string>()))
                .Returns("TestBrowser");

            _userAgentParserMock
                .Setup(parser => parser.GetDeviceInfo(It.IsAny<string>()))
                .Returns("TestDevice");

            // Act
            await _interactor.ExecuteAsync(input, CancellationToken.None);

            // Assert
            _transactionServiceMock.Verify(t => t.ExecuteAsync<Task>(It.IsAny<Func<Task<Task>>>(), It.IsAny<CancellationToken>()), Times.Once);
            _userRepositoryMock.Verify(repo => repo.GetByIdAsync(userId, It.IsAny<CancellationToken>()), Times.Once);
            _passwordServiceMock.Verify(service => service.VerifyPassword(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            _passwordServiceMock.Verify(service => service.HashPassword(input.NewPassword), Times.Once);
            _passwordServiceMock.Verify(service => service.IsPasswordStrong(input.NewPassword), Times.Once);
            _userRepositoryMock.Verify(repo => repo.Update(It.Is<User>(u => u.PasswordHash == "hashedNewPassword")), Times.Once);
            _loginHistoryRepositoryMock.Verify(repo => repo.AddAsync(
                It.Is<LoginHistory>(lh => lh.IsSuccess == true && lh.FailureReason == null), 
                It.IsAny<CancellationToken>()), Times.Once);
            _userRepositoryMock.Verify(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            _loginHistoryRepositoryMock.Verify(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            _historyServiceMock.Verify(service => service.CreateUserHistoryAsync(
                It.IsAny<User>(),
                OperationType.Update,
                input.UserId,
                input.IpAddress,
                input.UserAgent,
                It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact(DisplayName = "正常系:パスワード変更時にユーザー履歴が作成されること")]
        public async Task ChangePassword_ShouldCreateUserHistory()
        {
            // TODO: 実装
        }

        [Fact(DisplayName = "正常系:パスワード変更成功時にログイン履歴が記録されること")]
        public async Task ChangePassword_ShouldCreateLoginHistoryOnSuccess()
        {
            // TODO: 実装
        }

        [Fact(DisplayName = "正常系:トランザクションが正常に実行されること")]
        public async Task ChangePassword_ShouldExecuteWithinTransaction()
        {
            // TODO: 実装
        }

        [Fact(DisplayName = "正常系:ユーザーの監査情報（UpdatedAt, UpdatedBy）が更新されること")]
        public async Task ChangePassword_ShouldUpdateAuditFields()
        {
            // TODO: 実装
        }

        // 異常系
        [Fact(DisplayName = "異常系:新しいパスワードと確認パスワードが一致しない場合、ValidationExceptionがスローされること")]
        public async Task ChangePassword_NewPasswordAndConfirmNotMatch_ShouldThrowValidationException()
        {
            // TODO: 実装
        }

        [Fact(DisplayName = "異常系:必須項目（CurrentPassword, NewPassword）が空の場合、ValidationExceptionがスローされること")]
        public async Task ChangePassword_RequiredFieldsEmpty_ShouldThrowValidationException()
        {
            // TODO: 実装
        }

        [Fact(DisplayName = "異常系:UserIdが空のGUIDの場合、InvalidArgumentsExceptionがスローされること")]
        public async Task ChangePassword_EmptyGuid_ShouldThrowInvalidArgumentsException()
        {
            // TODO: 実装
        }

        [Fact(DisplayName = "異常系:ユーザーが存在しない場合、NotFoundExceptionがスローされること")]
        public async Task ChangePassword_UserNotFound_ShouldThrowNotFoundException()
        {
            // TODO: 実装
        }

        [Fact(DisplayName = "異常系:ユーザーが非アクティブの場合、NotFoundExceptionがスローされること")]
        public async Task ChangePassword_UserInactive_ShouldThrowNotFoundException()
        {
            // TODO: 実装
        }

        [Fact(DisplayName = "異常系:現在のパスワードが間違っている場合、UnauthorizedExceptionがスローされること")]
        public async Task ChangePassword_CurrentPasswordInvalid_ShouldThrowUnauthorizedException()
        {
            // TODO: 実装
        }

        [Fact(DisplayName = "異常系:新しいパスワードが現在のパスワードと同じ場合、ValidationExceptionがスローされること")]
        public async Task ChangePassword_NewPasswordSameAsCurrent_ShouldThrowValidationException()
        {
            // TODO: 実装
        }

        [Fact(DisplayName = "異常系:新しいパスワードが弱い場合、ValidationExceptionがスローされること")]
        public async Task ChangePassword_WeakNewPassword_ShouldThrowValidationException()
        {
            // TODO: 実装
        }

        [Fact(DisplayName = "異常系:パスワード検証失敗時に失敗ログイン履歴が記録されること")]
        public async Task ChangePassword_VerifyPasswordFailed_ShouldCreateFailedLoginHistory()
        {
            // TODO: 実装
        }

        [Fact(DisplayName = "異常系:トランザクション内で例外が発生した場合、ロールバックされること")]
        public async Task ChangePassword_ExceptionInTransaction_ShouldRollback()
        {
            // TODO: 実装
        }
    }
}
