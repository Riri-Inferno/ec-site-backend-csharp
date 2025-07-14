using EcSiteBackend.Domain.Entities;

namespace EcSiteBackend.Application.Common.Interfaces.Repositories
{
    /// <summary>
    /// パスワードリセットトークンリポジトリのインターフェース
    /// </summary>
    public interface IPasswordResetTokenRepository : IGenericRepository<PasswordResetToken>
    {
        /// <summary>
        /// 有効なトークンを取得する
        /// </summary>
        /// <param name="tokenHash">トークンのハッシュ値</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>有効なトークン、存在しない場合はnull</returns>
        Task<PasswordResetToken?> GetValidTokenAsync(string tokenHash, CancellationToken cancellationToken = default);

        /// <summary>
        /// ユーザーの既存トークンを無効化する
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        Task InvalidateUserTokensAsync(Guid userId, CancellationToken cancellationToken = default);

        /// <summary>
        /// トークンを使用済みにする
        /// </summary>
        /// <param name="tokenId">トークンID</param>
        /// <param name="usedIpAddress">使用時のIPアドレス</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        Task MarkAsUsedAsync(Guid tokenId, string? usedIpAddress, CancellationToken cancellationToken = default);
    }
}
