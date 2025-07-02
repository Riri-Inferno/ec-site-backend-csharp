using EcSiteBackend.Domain.Entities;
using System.Security.Claims;

namespace EcSiteBackend.Application.Common.Interfaces
{
    /// <summary>
    /// JWTトークンの生成と検証を行うサービスのインターフェース
    /// 
    /// </summary>
    public interface IJwtService
    {
        string GenerateToken(User user);
        ClaimsPrincipal? ValidateToken(string token);
    }
}
