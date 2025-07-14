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

        [Fact(DisplayName = "正常系:ユーザー情報（FirstName）のみ更新できること")]
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
                    It.IsAny<string>(),
                    It.IsAny<string>(),
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
            Assert.Equal(expectedUserDto.LastName, result.LastName);
            Assert.Equal(expectedUserDto.PhoneNumber, result.PhoneNumber);

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
                "1.11.1111",
                "userAgent",
                It.IsAny<CancellationToken>()), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<UserDto>(It.IsAny<User>()), Times.Once);
        }

        [Fact(DisplayName = "正常系:ユーザー情報（LastName）のみ更新できること")]
        public async Task UpdateUser_LastNameOnly_ShouldUpdateSuccessfully()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var input = new UpdateUserInput
            {
                Id = userId,
                LastName = "UpdatedLastName"
            };

            // 既存のユーザー
            var existingUser = new User
            {
                Id = userId,
                FirstName = "変更なし",
                LastName = "OldLastName",
                Email = "never.change@zmail.com",
                PhoneNumber = "111-1111-1111",
                CreatedAt = DateTime.UtcNow.AddDays(-1),
                IsActive = true
            };

            // 期待される帰り値
            var expectedUserDto = new UserDto
            {
                Id = userId,
                FirstName = "変更なし",
                LastName = "UpdatedLastName",
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
                    It.IsAny<string>(),
                    It.IsAny<string>(),
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
            Assert.Equal(expectedUserDto.LastName, result.LastName);
            Assert.Equal(expectedUserDto.PhoneNumber, result.PhoneNumber);

            // 依存関係呼び出しの検証
            _userRepositoryMock.Verify(repo => repo.GetByIdAsync(userId, It.IsAny<CancellationToken>()), Times.Once);
            _userRepositoryMock.Verify(repo => repo.Update(It.Is<User>(u =>
                u.Id == userId &&
                u.FirstName == "変更なし" &&
                u.LastName == "UpdatedLastName")), Times.Once);
            _userRepositoryMock.Verify(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            _historyServiceMock.Verify(service => service.CreateUserHistoryAsync(
                It.IsAny<User>(),
                OperationType.Update,
                userId,
                "1.12.12",
                "useragent",
                It.IsAny<CancellationToken>()), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<UserDto>(It.IsAny<User>()), Times.Once);
        }

        [Fact(DisplayName = "正常系:ユーザー情報（PhoneNumber）のみ更新できること")]
        public async Task UpdateUser_PhoneNumberOnly_ShouldUpdateSuccessfully()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var input = new UpdateUserInput
            {
                Id = userId,
                PhoneNumber = "222-2222-2222"
            };

            // 既存のユーザー
            var existingUser = new User
            {
                Id = userId,
                FirstName = "変更なし",
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
                FirstName = "変更なし",
                LastName = "変更なし",
                Email = "never.change@zmail.com",
                PhoneNumber = "222-2222-2222"
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
                    It.IsAny<string>(),
                    It.IsAny<string>(),
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
            Assert.Equal(expectedUserDto.LastName, result.LastName);
            Assert.Equal(expectedUserDto.PhoneNumber, result.PhoneNumber);

            // 依存関係呼び出しの検証
            _userRepositoryMock.Verify(repo => repo.GetByIdAsync(userId, It.IsAny<CancellationToken>()), Times.Once);
            _userRepositoryMock.Verify(repo => repo.Update(It.Is<User>(u =>
                u.Id == userId &&
                u.FirstName == "変更なし" &&
                u.LastName == "変更なし" &&
                u.PhoneNumber == "222-2222-2222")), Times.Once);
            _userRepositoryMock.Verify(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            _historyServiceMock.Verify(service => service.CreateUserHistoryAsync(
                It.IsAny<User>(),
                OperationType.Update,
                userId,
                "1.12.1",
                "userAgent",
                It.IsAny<CancellationToken>()), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<UserDto>(It.IsAny<User>()), Times.Once);
        }

        [Fact(DisplayName = "正常系:複数フィールドを同時に更新できること")]
        public async Task UpdateUser_MultipleFields_ShouldUpdateSuccessfully()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var input = new UpdateUserInput
            {
                Id = userId,
                FirstName = "UpdatedFirstName",
                LastName = "UpdatedLastName",
                PhoneNumber = "222-2222-2222"
            };

            // 既存のユーザー
            var existingUser = new User
            {
                Id = userId,
                FirstName = "John",
                LastName = "Doe",
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
                LastName = "UpdatedLastName",
                Email = "never.change@zmail.com",
                PhoneNumber = "222-2222-2222"
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
                    It.IsAny<string>(),
                    It.IsAny<string>(),
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
            Assert.Equal(expectedUserDto.LastName, result.LastName);
            Assert.Equal(expectedUserDto.PhoneNumber, result.PhoneNumber);

            // 依存関係呼び出しの検証
            _userRepositoryMock.Verify(repo => repo.GetByIdAsync(userId, It.IsAny<CancellationToken>()), Times.Once);
            _userRepositoryMock.Verify(repo => repo.Update(It.Is<User>(u =>
                u.Id == userId &&
                u.FirstName == "UpdatedFirstName" &&
                u.LastName == "UpdatedLastName" &&
                u.PhoneNumber == "222-2222-2222")), Times.Once);
            _userRepositoryMock.Verify(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            _historyServiceMock.Verify(service => service.CreateUserHistoryAsync(
                It.IsAny<User>(),
                OperationType.Update,
                userId,
                "1.12.2",
                "userAgent",
                It.IsAny<CancellationToken>()), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<UserDto>(It.IsAny<User>()), Times.Once);
        }

        [Fact(DisplayName = "正常系:空文字列/nullの場合は既存値が保持されること")]
        public async Task UpdateUser_EmptyOrNullInput_ShouldKeepExistingValues()
        {
            // Arrange
            var userId = Guid.NewGuid();

            // 更新なし
            var input = new UpdateUserInput
            {
                Id = userId,
            };

            // 既存のユーザー
            var existingUser = new User
            {
                Id = userId,
                FirstName = "John",
                LastName = "Doe",
                Email = "never.change@zmail.com",
                PhoneNumber = "111-1111-1111",
                CreatedAt = DateTime.UtcNow.AddDays(-1),
                IsActive = true
            };

            // 期待される帰り値
            var expectedUserDto = new UserDto
            {
                Id = userId,
                FirstName = "John",
                LastName = "Doe",
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
                    It.IsAny<string>(),
                    It.IsAny<string>(),
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
            Assert.Equal(expectedUserDto.LastName, result.LastName);
            Assert.Equal(expectedUserDto.PhoneNumber, result.PhoneNumber);

            // 依存関係呼び出しの検証
            _userRepositoryMock.Verify(repo => repo.GetByIdAsync(userId, It.IsAny<CancellationToken>()), Times.Once);
            _userRepositoryMock.Verify(repo => repo.Update(It.Is<User>(u =>
                u.Id == userId &&
                u.FirstName == "John" &&
                u.LastName == "Doe" &&
                u.PhoneNumber == "111-1111-1111")), Times.Once);
            _userRepositoryMock.Verify(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            _historyServiceMock.Verify(service => service.CreateUserHistoryAsync(
                It.IsAny<User>(),
                OperationType.Update,
                userId,
                "1.23.45",
                "userAgent",
                It.IsAny<CancellationToken>()), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<UserDto>(It.IsAny<User>()), Times.Once);
        }

        [Fact(DisplayName = "正常系:更新履歴が正しく作成されること")]
        public async Task UpdateUser_ShouldCreateHistory()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var input = new UpdateUserInput
            {
                Id = userId,
                FirstName = "NewFirstName",
                Email = "new.email@example.com"
            };

            var existingUser = new User
            {
                Id = userId,
                FirstName = "OldFirstName",
                LastName = "TestLastName",
                Email = "old.email@example.com",
                PhoneNumber = "090-1234-5678",
                CreatedAt = DateTime.UtcNow.AddDays(-1),
                IsActive = true
            };

            _userRepositoryMock
                .Setup(repo => repo.GetByIdAsync(userId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingUser);

            _transactionServiceMock
                .Setup(t => t.ExecuteAsync(It.IsAny<Func<Task>>(), It.IsAny<CancellationToken>()))
                .Returns<Func<Task>, CancellationToken>(async (action, ct) => await action());

            // Act
            await _interactor.ExecuteAsync(input, CancellationToken.None);

            // Assert - 履歴サービスが正しいパラメータで呼ばれたことを確認
            _historyServiceMock.Verify(service => service.CreateUserHistoryAsync(
                It.Is<User>(u => 
                    u.FirstName == "OldFirstName" && 
                    u.Email == "old.email@example.com"),
                OperationType.Update,
                userId,
                "1.23.45",
                "userAgent",
                It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact(DisplayName = "正常系:監査情報（UpdatedAt/UpdatedBy）が設定されること")]
        public async Task UpdateUser_ShouldSetAuditFields()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var input = new UpdateUserInput
            {
                Id = userId,
                FirstName = "NewFirstName"
            };

            var existingUser = new User
            {
                Id = userId,
                FirstName = "OldFirstName",
                LastName = "TestLastName",
                Email = "test@example.com",
                PhoneNumber = "090-1234-5678",
                CreatedAt = DateTime.UtcNow.AddDays(-1),
                IsActive = true
            };

            User updatedUser = null!;

            _userRepositoryMock
                .Setup(repo => repo.GetByIdAsync(userId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingUser);

            _userRepositoryMock
                .Setup(repo => repo.Update(It.IsAny<User>()))
                .Callback<User>(u => updatedUser = u);

            _transactionServiceMock
                .Setup(t => t.ExecuteAsync(It.IsAny<Func<Task>>(), It.IsAny<CancellationToken>()))
                .Returns<Func<Task>, CancellationToken>(async (action, ct) => await action());

            // Act
            await _interactor.ExecuteAsync(input, CancellationToken.None);

            // Assert
            Assert.NotNull(updatedUser);
            Assert.NotNull(updatedUser.UpdatedAt);
            Assert.Equal(userId, updatedUser.UpdatedBy);
            Assert.True(updatedUser.UpdatedAt > existingUser.CreatedAt);
        }

        [Fact(DisplayName = "正常系:TransactionServiceのExecuteAsyncメソッドが呼ばれること")]
        public async Task UpdateUser_ShouldCallTransactionService()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var input = new UpdateUserInput
            {
                Id = userId,
                FirstName = "NewFirstName"
            };

            var existingUser = new User
            {
                Id = userId,
                FirstName = "OldFirstName",
                LastName = "TestLastName",
                Email = "test@example.com",
                PhoneNumber = "090-1234-5678",
                CreatedAt = DateTime.UtcNow.AddDays(-1),
                IsActive = true
            };

            _userRepositoryMock
                .Setup(repo => repo.GetByIdAsync(userId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingUser);

            var transactionExecuted = false;
            _transactionServiceMock
                .Setup(t => t.ExecuteAsync(It.IsAny<Func<Task>>(), It.IsAny<CancellationToken>()))
                .Returns<Func<Task>, CancellationToken>(async (action, ct) => 
                {
                    transactionExecuted = true;
                    await action();
                });

            // Act
            await _interactor.ExecuteAsync(input, CancellationToken.None);

            // Assert
            Assert.True(transactionExecuted);
            _transactionServiceMock.Verify(
                t => t.ExecuteAsync(It.IsAny<Func<Task>>(), It.IsAny<CancellationToken>()), 
                Times.Once);
        }

        [Fact(DisplayName = "異常系:存在しないユーザーIDの場合、NotFoundExceptionがスローされること")]
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

        [Fact(DisplayName = "異常系:ユーザーIDがGuid.Emptyの場合、NotFoundExceptionがスローされること")]
        public async Task UpdateUser_EmptyGuid_ShouldThrowNotFoundException()
        {
            // Arrange
            var userId = Guid.Empty;
            var input = new UpdateUserInput
            {
                Id = userId,
                FirstName = "UpdateFirstName"
            };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidArgumentsException>(
                () => _interactor.ExecuteAsync(input, CancellationToken.None));

            Assert.Contains("無効な引数が指定されました", exception.Message);

            // 依存関係は呼ばれないことを確認
            _userRepositoryMock.Verify(repo => repo.GetByIdAsync(userId, It.IsAny<CancellationToken>()), Times.Never);
            _userRepositoryMock.Verify(repo => repo.Update(It.IsAny<User>()), Times.Never);
            _userRepositoryMock.Verify(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact(DisplayName = "異常系:リポジトリのUpdate時にエラーが発生した場合、後続処理がスキップされること")]
        public async Task UpdateUser_RepositoryUpdateError_ShouldSkipSubsequentOperations()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var input = new UpdateUserInput
            {
                Id = userId,
                FirstName = "ErrorFirstName"
            };

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

            _userRepositoryMock
                .Setup(repo => repo.GetByIdAsync(userId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingUser);

            _userRepositoryMock
                .Setup(repo => repo.Update(It.IsAny<User>()))
                .Throws(new Exception("Repository update error"));

            _transactionServiceMock
                .Setup(t => t.ExecuteAsync(It.IsAny<Func<Task>>(), It.IsAny<CancellationToken>()))
                .Returns<Func<Task>, CancellationToken>(async (action, ct) =>
                {
                    await action();
                });

            // Act & Assert
            var ex = await Assert.ThrowsAsync<Exception>(() => _interactor.ExecuteAsync(input, CancellationToken.None));
            Assert.Equal("Repository update error", ex.Message);

            // SaveChangesAsyncや履歴サービスは呼ばれないこと
            _userRepositoryMock.Verify(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
            _historyServiceMock.Verify(service => service.CreateUserHistoryAsync(
                It.IsAny<User>(),
                It.IsAny<OperationType>(),
                It.IsAny<Guid>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact(DisplayName = "異常系:履歴サービスでエラーが発生した場合、例外が伝播すること")]
        public async Task UpdateUser_HistoryServiceError_ShouldPropagateException()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var input = new UpdateUserInput
            {
                Id = userId,
                FirstName = "ErrorFirstName"
            };

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

            _userRepositoryMock
                .Setup(repo => repo.GetByIdAsync(userId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingUser);

            _userRepositoryMock
                .Setup(repo => repo.Update(It.IsAny<User>()));

            _historyServiceMock
                .Setup(service => service.CreateUserHistoryAsync(
                    It.IsAny<User>(),
                    It.IsAny<OperationType>(),
                    It.IsAny<Guid>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .Throws(new Exception("History service error"));

            _transactionServiceMock
                .Setup(t => t.ExecuteAsync(It.IsAny<Func<Task>>(), It.IsAny<CancellationToken>()))
                .Returns<Func<Task>, CancellationToken>(async (action, ct) =>
                {
                    await action();
                });

            // Act & Assert
            var ex = await Assert.ThrowsAsync<Exception>(() => _interactor.ExecuteAsync(input, CancellationToken.None));
            Assert.Equal("History service error", ex.Message);

            // SaveChangesAsyncは呼ばれる
            _userRepositoryMock.Verify(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
