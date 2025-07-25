using EcSiteBackend.Application.UseCases.Interfaces;
using EcSiteBackend.Application.Common.Interfaces.Repositories;
using AutoMapper;
using EcSiteBackend.Application.DTOs;
using EcSiteBackend.Application.Common.Exceptions;
using Microsoft.Extensions.Logging;

namespace EcSiteBackend.Application.UseCases.Interactors
{
    /// <summary>
    /// 現在のユーザーを読み取るユースケースの実装
    /// </summary>
    public class ReadCurrentUserInteractor : IReadCurrentUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ReadCurrentUserInteractor> _logger;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="userRepository">ユーザーリポジトリ</param>
        /// <param name="mapper">AutoMapperインスタンス</param>
        public ReadCurrentUserInteractor(
            IUserRepository userRepository,
            IMapper mapper,
            ILogger<ReadCurrentUserInteractor> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// 現在のユーザー情報を取得する
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        public async Task<UserDto> ExecuteAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            // TODO:ログは後ほど共通化する
            _logger.LogInformation("Fetching current user information for UserId: {UserId}", userId);

            // ユーザーIDが無効な場合は例外をスロー
            if (userId == Guid.Empty)
            {
                _logger.LogWarning("Invalid user ID provided: {UserId}", userId);
                throw new ArgumentException("Invalid user ID provided.", nameof(userId));
            }

            // ユーザーIDが無効な場合は例外をスロー
            var user = await _userRepository.GetByIdAsync(userId, cancellationToken);

            // ユーザーが見つからない場合は例外をスロー
            if (user is null)
            {
                _logger.LogWarning("User not found. UserId: {UserId}", userId);
                throw new NotFoundException($"User with ID {userId} not found.");
            }

            // ユーザーがアクティブでない場合は例外をスロー
            if (!user.IsActive)
            {
                _logger.LogWarning("Inactive user attempted to access. UserId: {UserId}", userId);
                throw new UnauthorizedException("User account is deactivated.");
            }

            _logger.LogInformation("Successfully retrieved user information. UserId: {UserId}", userId);

            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }
    }
}
