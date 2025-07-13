using AutoMapper;
using EcSiteBackend.Application.Common.Interfaces.Repositories;
using EcSiteBackend.Application.Common.Interfaces.Services;
using EcSiteBackend.Domain.Entities;
using EcSiteBackend.Domain.Enums;

namespace EcSiteBackend.Infrastructure.Services
{
    /// <summary>
    /// 履歴管理サービス
    /// </summary>
    public class HistoryService : IHistoryService
    {
        private readonly IHistoryRepository<UserHistory> _userHistoryRepository;
        private readonly IMapper _mapper;

        public HistoryService(
            IHistoryRepository<UserHistory> userHistoryRepository,
            IMapper mapper)
        {
            _userHistoryRepository = userHistoryRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// ユーザー履歴を作成
        /// </summary>
        public async Task CreateUserHistoryAsync(
            User user,
            OperationType operationType,
            Guid operatedBy,
            CancellationToken cancellationToken = default)
        {
            var history = _mapper.Map<UserHistory>(user);
            history.Id = Guid.NewGuid();
            history.OriginalId = user.Id;
            history.OperationType = operationType;
            history.OperatedBy = operatedBy;
            history.OperatedAt = DateTime.UtcNow;

            await _userHistoryRepository.AddAsync(history, cancellationToken);
            await _userHistoryRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
