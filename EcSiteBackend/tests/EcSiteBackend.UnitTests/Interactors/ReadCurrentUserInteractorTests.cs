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
using Microsoft.Extensions.Logging;

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

        }

        [Fact(DisplayName = "Guid.Emptyが渡された場合、ArgumentExceptionがスローされること")]
        public async Task ExecuteAsync_EmptyGuid_ThrowsArgumentException()
        {

        }

        [Fact(DisplayName = "存在しないユーザーIDの場合、NotFoundExceptionがスローされること")]
        public async Task ExecuteAsync_NonExistentUserId_ThrowsNotFoundException()
        {

        }

        [Fact(DisplayName = "非アクティブユーザーの場合、UnauthorizedExceptionがスローされること")]
        public async Task ExecuteAsync_InactiveUser_ThrowsUnauthorizedException()
        {
            
        }
    }
}