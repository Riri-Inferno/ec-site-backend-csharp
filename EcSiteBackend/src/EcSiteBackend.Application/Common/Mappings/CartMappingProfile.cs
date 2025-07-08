using AutoMapper;
using EcSiteBackend.Domain.Entities;

namespace EcSiteBackend.Application.Common.Mappings
{
    /// <summary>
    /// カートのマッピング設定
    /// </summary>

    public class CartMappingProfile : Profile
    {
        public CartMappingProfile()
        {
            // 監査フィールドを設定
            CreateMap<Cart, Cart>()
                .ConfigureAuditableEntity();
        }
    }
}
