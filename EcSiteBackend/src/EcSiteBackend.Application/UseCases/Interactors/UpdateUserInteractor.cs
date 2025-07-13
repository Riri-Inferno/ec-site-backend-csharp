using EcSiteBackend.Application.UseCases.Interfaces;
using EcSiteBackend.Application.Common.Interfaces.Repositories;
using AutoMapper;
using EcSiteBackend.Application.Common.Exceptions;
using EcSiteBackend.Application.UseCases.InputOutputModels;
using EcSiteBackend.Domain.Entities;
using EcSiteBackend.Application.Common.Interfaces.Services;
using EcSiteBackend.Application.Common.Interfaces;
using EcSiteBackend.Domain.Enums;
using EcSiteBackend.Application.DTOs;
using EcSiteBackend.Application.Common.Constants;
using EcSiteBackend.Application.Common.Extensions;

namespace EcSiteBackend.Application.UseCases.Interactors
{
    /// <summary>
    /// ユーザー情報を更新するユースケースの実装
    /// </summary>
    public class UpdateUserInteractor : IUpdateUserUseCase
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IHistoryService _historyService;
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UpdateUserInteractor(
            IGenericRepository<User> userRepository,
            IHistoryService historyService,
            ITransactionService transactionService,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _historyService = historyService;
            _transactionService = transactionService;
            _mapper = mapper;
        }

        /// <summary>
        /// ユーザー情報を更新する
        /// </summary>
        public async Task<UserDto> ExecuteAsync(UpdateUserInput input, CancellationToken cancellationToken)
        {
            var user = new User();

            await _transactionService.ExecuteAsync(async () =>
            {
                // 更新対象を取得
                user = await _userRepository.GetByIdAsync(input.Id, cancellationToken);
                if (user is null)
                {
                    throw new NotFoundException(ErrorCodes.NotFound, $"User (ID: {input.Id}) が見つかりません。");
                }

                // 履歴保存用のクローンを作成
                var originalUser = user.CloneForHistory();

                // 入力データで更新
                if (!string.IsNullOrEmpty(input.Email))
                    user.Email = input.Email;
                if (!string.IsNullOrEmpty(input.FirstName))
                    user.FirstName = input.FirstName;
                if (!string.IsNullOrEmpty(input.LastName))
                    user.LastName = input.LastName;
                if (!string.IsNullOrEmpty(input.PhoneNumber))
                    user.PhoneNumber = input.PhoneNumber;

                // 監査情報を設定
                user.MarkAsUpdated(input.Id); // または適切なユーザーID

                _userRepository.Update(user);
                await _userRepository.SaveChangesAsync(cancellationToken);

                // 履歴を作成
                await _historyService.CreateUserHistoryAsync(
                    originalUser,
                    OperationType.Update,
                    input.Id,
                    cancellationToken);
            }, cancellationToken);

            // 更新後のユーザー情報を返す
            return _mapper.Map<UserDto>(user);
        }
    }
}
