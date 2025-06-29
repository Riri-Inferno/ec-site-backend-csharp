using EcSiteBackend.Application.UseCases.Interactors;
using EcSiteBackend.Application.Common.Interfaces.Repositories;
using EcSiteBackend.Application.Common.Interfaces;
using AutoMapper;
using Moq;
using EcSiteBackend.Application.UseCases.InputOutputModels;
using EcSiteBackend.Domain.Entities;

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
    }
}
