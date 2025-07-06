using AutoMapper;
using EcSiteBackend.Application.Common.Interfaces.Repositories;
using EcSiteBackend.Domain.Entities;
using EcSiteBackend.Domain.Enums;

namespace EcSiteBackend.Application.Common.Interfaces.Services
{
    /// <summary>
    /// 履歴管理サービスのインターフェース
    /// </summary>
    public interface IHistoryService
    {
        /// <summary>
        /// ユーザー履歴を作成
        /// </summary>
        /// <param name="user"></param>
        /// <param name="operationType"></param>
        /// <param name="operatedBy"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task CreateUserHistoryAsync(User user, OperationType operationType, Guid operatedBy, CancellationToken cancellationToken = default);
    }
}
