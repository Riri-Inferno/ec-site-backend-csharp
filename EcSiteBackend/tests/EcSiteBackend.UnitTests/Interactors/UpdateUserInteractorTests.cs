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
using EcSiteBackend.Application.Common.Interfaces.Services;
using Microsoft.Extensions.Options;
using EcSiteBackend.Application.Common.Settings;
using EcSiteBackend.Domain.Enums;

namespace EcSiteBackend.UnitTests.Interactors
{
    /// <summary>
    /// ユーザー情報更新ユースケースのテストクラス
    /// </summary>
    public class UpdateUserInteractorTests
    {
        private readonly Mock<IGenericRepository<User>> _userRepositoryMock;
        private readonly Mock<IHistoryService> _historyServiceMock;
        private readonly Mock<ITransactionService> _transactionServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly UpdateUserInteractor _interactor;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UpdateUserInteractorTests()
        {
            _userRepositoryMock = new Mock<IGenericRepository<User>>();
            _historyServiceMock = new Mock<IHistoryService>();
            _transactionServiceMock = new Mock<ITransactionService>();
            _mapperMock = new Mock<IMapper>();

            _interactor = new UpdateUserInteractor(
                _userRepositoryMock.Object,
                _historyServiceMock.Object,
                _transactionServiceMock.Object,
                _mapperMock.Object);
        }

        [Fact(DisplayName = "ユーザー情報（FirstName）のみ更新できること")]
        public async Task UpdateUser_FirstNameOnly_ShouldUpdateSuccessfully()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var input = new UpdateUserInput
            {
                Id = userId,
                FirstName = "UpdatedFirstName"
            };

            // 既存のユーザー
            var existingUser = new User
            {
                Id = userId,
                FirstName = "OldFirstName",
                LastName = "変更なし",
                Email = "never.change@zmail.com",
                PhoneNumber = "111-1111-1111",
                CreatedAt = DateTime.UtcNow.AddDays(-1),
                IsActive = true
            };

            // 期待される帰り値
            var expectedUserDto = new UserDto
            {
                Id = userId,
                FirstName = "UpdatedFirstName",
                LastName = "変更なし",
                Email = "never.change@zmail.com",
                PhoneNumber = "111-1111-1111"
            };

            // モックの設定
            _userRepositoryMock
                .Setup(repo => repo.GetByIdAsync(userId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingUser);

            _userRepositoryMock
                .Setup(repo => repo.Update(It.IsAny<User>()));

            _userRepositoryMock
                .Setup(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            _mapperMock
                .Setup(mapper => mapper.Map<UserDto>(It.IsAny<User>()))
                .Returns(expectedUserDto);

            _historyServiceMock
                .Setup(service => service.CreateUserHistoryAsync(
                    It.IsAny<User>(),
                    It.IsAny<OperationType>(),
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // TransactionServiceのモックを修正
            _transactionServiceMock
                .Setup(t => t.ExecuteAsync(It.IsAny<Func<Task>>(), It.IsAny<CancellationToken>()))
                .Returns<Func<Task>, CancellationToken>(async (action, ct) =>
                {
                    await action();
                });

            // Act
            var result = await _interactor.ExecuteAsync(input, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedUserDto.Id, result.Id);
            Assert.Equal(expectedUserDto.FirstName, result.FirstName);

            // 依存関係呼び出しの検証
            _userRepositoryMock.Verify(repo => repo.GetByIdAsync(userId, It.IsAny<CancellationToken>()), Times.Once);
            _userRepositoryMock.Verify(repo => repo.Update(It.Is<User>(u =>
                u.Id == userId &&
                u.FirstName == "UpdatedFirstName" &&
                u.LastName == "変更なし")), Times.Once);
            _userRepositoryMock.Verify(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            _historyServiceMock.Verify(service => service.CreateUserHistoryAsync(
                It.IsAny<User>(),
                OperationType.Update,
                userId,
                It.IsAny<CancellationToken>()), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<UserDto>(It.IsAny<User>()), Times.Once);
        }

        [Fact(DisplayName = "存在しないユーザーIDの場合、NotFoundExceptionがスローされること")]
        public async Task UpdateUser_UserNotFound_ShouldThrowNotFoundException()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var input = new UpdateUserInput
            {
                Id = userId,
                FirstName = "UpdatedFirstName"
            };

            _userRepositoryMock
                .Setup(repo => repo.GetByIdAsync(userId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((User)null!);

            _transactionServiceMock
                .Setup(t => t.ExecuteAsync(It.IsAny<Func<Task>>(), It.IsAny<CancellationToken>()))
                .Returns<Func<Task>, CancellationToken>(async (action, ct) =>
                {
                    await action();
                });

            // Act & Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(
                () => _interactor.ExecuteAsync(input, CancellationToken.None));

            Assert.Contains($"User (ID: {userId}) が見つかりません。", exception.Message);

            // リポジトリが呼ばれたことを確認
            _userRepositoryMock.Verify(repo => repo.GetByIdAsync(userId, It.IsAny<CancellationToken>()), Times.Once);
            // 更新系のメソッドは呼ばれないことを確認
            _userRepositoryMock.Verify(repo => repo.Update(It.IsAny<User>()), Times.Never);
            _userRepositoryMock.Verify(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
