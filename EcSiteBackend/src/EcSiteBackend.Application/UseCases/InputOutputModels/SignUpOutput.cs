using EcSiteBackend.Application.DTOs;

namespace EcSiteBackend.Application.UseCases.InputOutputModels
{
    /// <summary>
    /// SignUp時の出力モデル
    /// </summary>
    public class SignUpOutput
    {
        public UserDto User { get; set; } = null!;
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
    }
}
