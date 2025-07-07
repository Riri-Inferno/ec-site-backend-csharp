namespace EcSiteBackend.Application.Common.Settings
{
    /// <summary>
    /// Jwtの設定
    /// </summary>
    public class JwtSettings
    {
        public string Secret { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public int ExpirationInMinutes { get; set; } = 60; // デフォルトは60分
    }
}
