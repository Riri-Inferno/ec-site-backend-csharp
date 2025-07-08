using AutoMapper;
using EcSiteBackend.Domain.Entities;

namespace EcSiteBackend.Application.Common.Mappings
{
    /// <summary>
    /// ログイン履歴のマッピング設定
    /// </summary>

    public class LoginHistoryMappingProfile : Profile
    {
        public LoginHistoryMappingProfile()
        {
            // 監査フィールドを設定
            CreateMap<LoginHistory, LoginHistory>()
                .ConfigureAuditableEntity()
                .ForMember(dest => dest.Id, opt => opt.Condition((src, dest) => dest.Id == Guid.Empty))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
