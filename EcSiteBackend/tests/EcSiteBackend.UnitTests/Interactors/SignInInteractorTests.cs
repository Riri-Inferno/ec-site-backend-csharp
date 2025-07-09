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
    }
}
