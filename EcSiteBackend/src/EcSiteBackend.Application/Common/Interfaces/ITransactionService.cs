namespace EcSiteBackend.Application.Common.Interfaces
{
    public interface ITransactionService
    {
        Task<T> ExecuteAsync<T>(Func<Task<T>> action, CancellationToken cancellationToken = default);
        Task ExecuteAsync(Func<Task> action, CancellationToken cancellationToken = default);
    }
}
