using EcSiteBackend.Application.UseCases.Interactors;
using EcSiteBackend.Application.Common.Interfaces.Repositories;
using EcSiteBackend.Application.Common.Interfaces;
using AutoMapper;
using Moq;
using EcSiteBackend.Application.UseCases.InputOutputModels;
using EcSiteBackend.Domain.Entities;
using EcSiteBackend.Application.DTOs;

namespace EcSiteBackend.Interactors.UnitTests
{
    /// <summary>
    /// ユーザー作成ユースケースのテストクラス
    /// </summary>
    public class CreateUserInteractorTests
    {
        private readonly CreateUserInteractor _interactor;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IPasswordHasher> _passwordHasherMock;
        private readonly Mock<IMapper> _mapperMock;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CreateUserInteractorTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _passwordHasherMock = new Mock<IPasswordHasher>();
            _mapperMock = new Mock<IMapper>();

            // インタララクをセットアップ
            _interactor = new CreateUserInteractor(
                _userRepositoryMock.Object,
                _passwordHasherMock.Object,
                _mapperMock.Object
            );
        }

        [Fact(DisplayName = "正常系：ユーザーが作成される")]
        public async Task ExecuteAsync_ValidInput_ReturnsUserDto()
        {
            // Arrange
            var input = new CreateUserInput
            {
                Email = "test.user@gmail.com",
                Password = "SecurePassword123",
                FirstName = "テスト",
                LastName = "太郎",
                PhoneNumber = "090-9999-9999"
            };

            _userRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            _userRepositoryMock.Setup(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            _mapperMock.Setup(mapper => mapper.Map<UserDto>(It.IsAny<User>()))
                .Returns(new UserDto
                {
                    Email = input.Email,
                    FirstName = input.FirstName,
                    LastName = input.LastName,
                    PhoneNumber = input.PhoneNumber
                });
            

            // Act
            var result = await _interactor.ExecuteAsync(input);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(input.Email, result.Email);
            Assert.Equal(input.FirstName, result.FirstName);
            Assert.Equal(input.LastName, result.LastName);
            Assert.Equal(input.PhoneNumber, result.PhoneNumber);

            _userRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);
            _userRepositoryMock.Verify(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Theory(DisplayName = "異常系：バリデーション例外が発生する")]
        [InlineData("", "password123", "名", "姓", "090-9999-9999", "メールアドレスは必須です。")]
        [InlineData("test.user@gmail.com", "", "名", "姓", "090-9999-9999", "パスワードは必須です。")]
        [InlineData("test.user@gmail.com", "short", "名", "姓", "090-9999-9999", "パスワードは8文字以上である必要があります。")]
        [InlineData("test.user@gmail.com", "password123", "", "姓", "090-9999-9999", "名は必須です。")]
        [InlineData("test.user@gmail.com", "password123", "名", "", "090-9999-9999", "姓は必須です。")]
        public async Task ExecuteAsync_InvalidInput_ThrowsArgumentException(string email, string password, string firstName, string lastName, string phoneNumber, string expectedMessage)
        {
            // Arrange
            var input = new CreateUserInput
            {
                Email = email,
                Password = password,
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNumber
            };

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => _interactor.ExecuteAsync(input));
            Assert.Equal(expectedMessage, ex.Message);
        }

        [Fact(DisplayName = "異常系：メールアドレス重複で例外が発生する")]
        public async Task ExecuteAsync_DuplicateEmail_ThrowsInvalidOperationException()
        {
            // Arrange
            var input = new CreateUserInput
            {
                Email = "duplicate.user@gmail.com",
                Password = "password123",
                FirstName = "名",
                LastName = "姓",
                PhoneNumber = "090-9999-9999"
            };

            _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(input.Email, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new User { Email = input.Email });

            // Act & Assert
            var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => _interactor.ExecuteAsync(input));
            Assert.Equal("このメールアドレスは既に使用されています。", ex.Message);
        }
    }
}
