using UAParser;
using EcSiteBackend.Application.Common.Interfaces;

namespace EcSiteBackend.Infrastructure.Services
{
    /// <summary>
    /// ユーザーエージェント文字列を解析し、ブラウザ・デバイス・OS情報を取得するサービス。
    /// </summary>
    public class UserAgentParser : IUserAgentParser
    {
        private readonly Parser _uaParser;

        /// <summary>
        /// UAParserライブラリのデフォルトインスタンスを初期化します。
        /// </summary>
        public UserAgentParser()
        {
            _uaParser = Parser.GetDefault();
        }

        /// <summary>
        /// ユーザーエージェントからブラウザ名を取得します。
        /// </summary>
        public string? GetBrowser(string? userAgent)
        {
            if (string.IsNullOrEmpty(userAgent)) return null;

            var clientInfo = _uaParser.Parse(userAgent);
            return $"{clientInfo.UA.Family} {clientInfo.UA.Major}.{clientInfo.UA.Minor}";
        }

        /// <summary>
        /// ユーザーエージェントからデバイス情報を取得します。
        /// </summary>
        public string? GetDeviceInfo(string? userAgent)
        {
            if (string.IsNullOrEmpty(userAgent)) return null;

            var clientInfo = _uaParser.Parse(userAgent);
            return clientInfo.Device.Family ?? "Unknown";
        }

        /// <summary>
        /// ユーザーエージェントからOS情報を取得します。
        /// </summary>
        public string? GetOperatingSystem(string? userAgent)
        {
            if (string.IsNullOrEmpty(userAgent)) return null;

            var clientInfo = _uaParser.Parse(userAgent);
            return $"{clientInfo.OS.Family} {clientInfo.OS.Major}.{clientInfo.OS.Minor}";
        }
    }
}
