using EcSiteBackend.Application.UseCases.Interfaces;
using EcSiteBackend.Application.Common.Interfaces.Repositories;
using AutoMapper;
using EcSiteBackend.Application.DTOs;
using EcSiteBackend.Application.Common.Exceptions;

namespace EcSiteBackend.Application.UseCases.Interactors
{
    /// <summary>
    /// 現在のユーザーを読み取るユースケースの実装
    /// </summary>
    public class ReadCurrentUserInteractor : IReadCurrentUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="userRepository">ユーザーリポジトリ</param>
        /// <param name="mapper">AutoMapperインスタンス</param>
        public ReadCurrentUserInteractor(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// 現在のユーザー情報を取得する
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        public async Task<UserDto> ExecuteAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            // ユーザーIDが無効な場合は例外をスロー
            var user = await _userRepository.GetByIdAsync(userId, cancellationToken);

            // ユーザーが見つからない場合は例外をスロー
            if (user is null)
            {
                throw new NotFoundException($"User with ID {userId} not found.");
            }

            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }
    }
}
