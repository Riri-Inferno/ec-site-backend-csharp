using EcSiteBackend.Application.UseCases.Interactors;
using EcSiteBackend.Application.Common.Interfaces.Repositories;
using AutoMapper;
using Moq;
using EcSiteBackend.Domain.Entities;
using EcSiteBackend.Application.DTOs;
using Microsoft.Extensions.Logging;
using EcSiteBackend.Application.Common.Exceptions;

namespace EcSiteBackend.UnitTests.Interactors
{
    /// <summary>
    /// ReadCurrentUserInteractorのテストクラス
    /// </summary>
    public class ReadCurrentUserInteractorTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILogger<ReadCurrentUserInteractor>> _loggerMock;
        private readonly ReadCurrentUserInteractor _interactor;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ReadCurrentUserInteractorTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<ReadCurrentUserInteractor>>();
            _interactor = new ReadCurrentUserInteractor(
                _userRepositoryMock.Object,
                _mapperMock.Object,
                _loggerMock.Object
            );
        }

        [Fact(DisplayName = "アクティブなユーザーが正常に取得できること")]
        public async Task ExecuteAsync_ActiveUser_ReturnsUserDto()
        {
            // Arrange
            var userId = Guid.NewGuid();

            var expectedUser = new User
            {
                Id = userId,
                PasswordHash = "hashed-password",
                Email = "john.doe@example.com",
                IsActive = true,
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "123-456-7890",
                EmailConfirmed = true,
                LastLoginAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                UserRoles = new List<UserRole>(),
                UserAddresses = new List<UserAddress>(),
                PasswordResetTokens = new List<PasswordResetToken>(),
            };

            var expectedUserDto = new UserDto
            {
                Id = expectedUser.Id,
                Email = expectedUser.Email,
                FirstName = expectedUser.FirstName,
                LastName = expectedUser.LastName,
                FullName = $"{expectedUser.FirstName} {expectedUser.LastName}",
                DisplayName = $"{expectedUser.FirstName} {expectedUser.LastName}",
                PhoneNumber = expectedUser.PhoneNumber,
                IsActive = expectedUser.IsActive,
                EmailConfirmed = expectedUser.EmailConfirmed,
                LastLoginAt = expectedUser.LastLoginAt,
                CreatedAt = expectedUser.CreatedAt,
                UpdatedAt = expectedUser.UpdatedAt,
            };

            _userRepositoryMock
                .Setup(repo => repo.GetByIdAsync(userId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedUser);

            _mapperMock
                .Setup(mapper => mapper.Map<UserDto>(expectedUser))
                .Returns(expectedUserDto);

            // Act
            var result = await _interactor.ExecuteAsync(userId, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedUserDto.Id, result.Id);
            Assert.Equal(expectedUserDto.Email, result.Email);
            Assert.Equal(expectedUserDto.FirstName, result.FirstName);
            Assert.Equal(expectedUserDto.LastName, result.LastName);
            Assert.Equal(expectedUserDto.FullName, result.FullName);
            Assert.Equal(expectedUserDto.DisplayName, result.DisplayName);
            Assert.Equal(expectedUserDto.PhoneNumber, result.PhoneNumber);
            Assert.Equal(expectedUserDto.IsActive, result.IsActive);
            Assert.Equal(expectedUserDto.EmailConfirmed, result.EmailConfirmed);
            Assert.Equal(expectedUserDto.LastLoginAt, result.LastLoginAt);
            Assert.Equal(expectedUserDto.CreatedAt, result.CreatedAt);
            Assert.Equal(expectedUserDto.UpdatedAt, result.UpdatedAt);

            _userRepositoryMock.Verify(
                repo => repo.GetByIdAsync(userId, It.IsAny<CancellationToken>()),
                Times.Once
            );

            _mapperMock.Verify(
                mapper => mapper.Map<UserDto>(expectedUser),
                Times.Once
            );

            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("Successfully retrieved user information")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once
            );
        }

        [Fact(DisplayName = "Guid.Emptyが渡された場合、ArgumentExceptionがスローされること")]
        public async Task ExecuteAsync_EmptyGuid_ThrowsArgumentException()
        {
            // Arrange
            var userId = Guid.Empty;

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(
                () => _interactor.ExecuteAsync(userId, CancellationToken.None)
            );

            Assert.Contains("Invalid user ID provided.", exception.Message);
            Assert.Equal("userId", exception.ParamName);

            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Warning,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("Invalid user ID provided")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once
            );

            _userRepositoryMock.Verify(
                repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()),
                Times.Never
            );

            _mapperMock.Verify(
                mapper => mapper.Map<UserDto>(It.IsAny<User>()),
                Times.Never
            );
        }

        [Fact(DisplayName = "存在しないユーザーIDの場合、NotFoundExceptionがスローされること")]
        public async Task ExecuteAsync_NonExistentUserId_ThrowsNotFoundException()
        {
            // Arrange
            var userId = Guid.NewGuid();

            _userRepositoryMock
                .Setup(repo => repo.GetByIdAsync(userId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((User?)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(
                () => _interactor.ExecuteAsync(userId, CancellationToken.None)
            );

            Assert.Equal($"User with ID {userId} not found.", exception.Message);

            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Warning,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("User not found")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once
            );

            _userRepositoryMock.Verify(
                repo => repo.GetByIdAsync(userId, It.IsAny<CancellationToken>()),
                Times.Once
            );

            _mapperMock.Verify(
                mapper => mapper.Map<UserDto>(It.IsAny<User>()),
                Times.Never
            );
        }

        [Fact(DisplayName = "非アクティブユーザーの場合、UnauthorizedExceptionがスローされること")]
        public async Task ExecuteAsync_InactiveUser_ThrowsUnauthorizedException()
        {
            // Arrange
            var userId = Guid.NewGuid();

            var expectedUser = new User
            {
                Id = userId,
                PasswordHash = "hashed-password",
                Email = "john.doe@example.com",
                IsActive = false, // 非アクティブユーザー
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "123-456-7890",
                EmailConfirmed = true,
                LastLoginAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                UserRoles = new List<UserRole>(),
                UserAddresses = new List<UserAddress>(),
                PasswordResetTokens = new List<PasswordResetToken>(),
            };

            _userRepositoryMock
                .Setup(repo => repo.GetByIdAsync(userId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedUser);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<UnauthorizedException>(
                () => _interactor.ExecuteAsync(userId, CancellationToken.None)
            );
            Assert.Equal("User account is deactivated.", exception.Message);

            _userRepositoryMock.Verify(
                repo => repo.GetByIdAsync(userId, It.IsAny<CancellationToken>()),
                Times.Once
            );

            _mapperMock.Verify(
                mapper => mapper.Map<UserDto>(It.IsAny<User>()),
                Times.Never
            );

            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Warning,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("Inactive user attempted to access")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once
            );
        }
    }
}
