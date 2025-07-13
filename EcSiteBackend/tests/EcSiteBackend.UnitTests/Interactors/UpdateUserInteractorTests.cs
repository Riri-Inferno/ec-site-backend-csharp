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
    }
}
