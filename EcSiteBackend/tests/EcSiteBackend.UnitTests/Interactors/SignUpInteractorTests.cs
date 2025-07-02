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
    /// ユーザー登録（サインアップ）ユースケースのテストクラス
    /// </summary>
    public class SignUpInteractorTests
    {
        // テスト対象のインタラクタ
        private readonly SignUpInteractor _interactor;
        // ユーザーリポジトリのモック
        private readonly Mock<IUserRepository> _userRepositoryMock;
        // カートリポジトリのモック
        private readonly Mock<IGenericRepository<Cart>> _cartRepositoryMock;
        // パスワードハッシャーのモック
        private readonly Mock<IPasswordHasher> _passwordHasherMock;
        // JWTサービスのモック
        private readonly Mock<IJwtService> _jwtServiceMock;
        // トランザクションサービスのモック
        private readonly Mock<ITransactionService> _transactionServiceMock;
        // AutoMapperのモック
        private readonly Mock<IMapper> _mapperMock;

        /// <summary>
        /// テストクラスのコンストラクタ。各依存サービスのモックを生成し、インタラクタに注入する。
        /// </summary>
        public SignUpInteractorTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _cartRepositoryMock = new Mock<IGenericRepository<Cart>>();
            _passwordHasherMock = new Mock<IPasswordHasher>();
            _jwtServiceMock = new Mock<IJwtService>();
            _transactionServiceMock = new Mock<ITransactionService>();
            _mapperMock = new Mock<IMapper>();

            _interactor = new SignUpInteractor(
                _userRepositoryMock.Object,
                _cartRepositoryMock.Object,
                _passwordHasherMock.Object,
                _jwtServiceMock.Object,
                _transactionServiceMock.Object,
                _mapperMock.Object
            );
        }
    }
}
