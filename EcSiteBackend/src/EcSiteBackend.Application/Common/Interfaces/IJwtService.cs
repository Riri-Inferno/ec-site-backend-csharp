using EcSiteBackend.Domain.Entities;
using System.Security.Claims;

namespace EcSiteBackend.Application.Common.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
        ClaimsPrincipal? ValidateToken(string token);
    }
}
