using AutoMapper;
using EcSiteBackend.Domain.Entities;

namespace EcSiteBackend.Application.Common.Mappings
{
    /// <summary>
    /// すべてのBaseEntityに共通するマッピング設定
    /// </summary>
    public class BaseEntityMappingProfile : Profile
    {
        public BaseEntityMappingProfile()
        {
            // すべてのBaseEntity派生クラスに適用される共通設定
            CreateMap<BaseEntity, BaseEntity>()
                .IncludeAllDerived();
        }
    }
}
