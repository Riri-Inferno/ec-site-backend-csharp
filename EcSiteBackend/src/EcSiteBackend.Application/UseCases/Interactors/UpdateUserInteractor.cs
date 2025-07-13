using EcSiteBackend.Application.UseCases.Interfaces;
using EcSiteBackend.Application.Common.Interfaces.Repositories;
using AutoMapper;
using EcSiteBackend.Application.Common.Exceptions;
using EcSiteBackend.Application.UseCases.InputOutputModels;
using EcSiteBackend.Domain.Entities;
using EcSiteBackend.Application.Common.Interfaces.Services;
using EcSiteBackend.Application.Common.Interfaces;
using EcSiteBackend.Domain.Enums;


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
        public async Task ExecuteAsync(UpdateUserInput input, CancellationToken cancellationToken)
        {
            await _transactionService.ExecuteAsync(async () =>
            {
                // 更新対象を取得
                var user = await _userRepository.GetByIdAsync(input.Id, cancellationToken);
                if (user is null)
                {
                    throw new NotFoundException("ユーザーが見つかりません");
                }

                // 履歴保存用
                var originalUser = _mapper.Map<User>(user);

                // 入力データをマッピングして更新
                _mapper.Map(input, user);
                _userRepository.Update(user);

                await _userRepository.SaveChangesAsync(cancellationToken);

                // 履歴を作成
                await _historyService.CreateUserHistoryAsync(
                    originalUser,
                    OperationType.Update,
                    input.Id,
                    cancellationToken);

            }, cancellationToken);
        }
    }
}
