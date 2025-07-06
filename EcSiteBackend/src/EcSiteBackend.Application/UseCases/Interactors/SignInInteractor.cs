using AutoMapper;
using EcSiteBackend.Application.Common.Constants;
using EcSiteBackend.Application.Common.Exceptions;
using EcSiteBackend.Application.Common.Interfaces;
using EcSiteBackend.Application.Common.Interfaces.Repositories;
using EcSiteBackend.Application.DTOs;
using EcSiteBackend.Application.UseCases.InputOutputModels;
using EcSiteBackend.Application.UseCases.Interfaces;
using EcSiteBackend.Domain.Entities;

namespace EcSiteBackend.Application.UseCases.Interactors
{
    /// <summary>
    /// サインインユースケースの実装
    /// </summary>
    public class SignInInteractor : ISignInUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;

        public SignInInteractor(IUserRepository userRepository, IJwtService jwtService, IMapper mapper)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _mapper = mapper;
        }

        /// <summary>
        /// サインインを実行する
        /// </summary>
        /// <param name="input">サインイン情報入力</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>サインインしたユーザー情報</returns>
        /// <exception cref="ValidationException">入力の検証に失敗した場合</exception>
        /// <exception cref="NotFoundException">ユーザーが見つからない場合  </exception>
        /// <exception cref="UnauthorizedException">認証に失敗した場合</exception>
        public async Task<AuthOutput> ExecuteAsync(SignInInput input, CancellationToken cancellationToken = default)
        {

        }
    }
}
