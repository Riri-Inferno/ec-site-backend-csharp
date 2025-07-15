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
    }
}
