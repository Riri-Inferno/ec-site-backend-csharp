using EcSiteBackend.Application.DTOs;

namespace EcSiteBackend.Application.UseCases.InputOutputModels
{
    /// <summary>
    /// 認証成功時の出力モデル（SignUp/SignIn共通）
    /// </summary>
    public class AuthOutput
    {
        public UserDto User { get; set; } = null!;
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
    }
}
