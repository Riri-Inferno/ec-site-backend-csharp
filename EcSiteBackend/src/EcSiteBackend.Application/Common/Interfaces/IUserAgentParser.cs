namespace EcSiteBackend.Application.Common.Interfaces
{
    /// <summary>
    /// ユーザーエージェント文字列を解析するためのインターフェース
    /// </summary>
    public interface IUserAgentParser
    {
        /// <summary>
        /// ユーザーエージェントからブラウザ名を取得する
        /// </summary>
        /// <param name="userAgent">ユーザーエージェント文字列</param>
        /// <returns>ブラウザ名</returns>
        string? GetBrowser(string? userAgent);

        /// <summary>
        /// ユーザーエージェントからデバイス情報を取得する
        /// </summary>
        /// <param name="userAgent">ユーザーエージェント文字列</param>
        /// <returns>デバイス情報</returns>
        string? GetDeviceInfo(string? userAgent);

        /// <summary>
        /// ユーザーエージェントからOS情報を取得する
        /// </summary>
        /// <param name="userAgent">ユーザーエージェント文字列</param>
        /// <returns>OS情報</returns>
        string? GetOperatingSystem(string? userAgent);
    }
}
