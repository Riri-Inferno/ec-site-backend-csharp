using AutoMapper;
using EcSiteBackend.Application.DTOs;
using EcSiteBackend.Domain.Entities;

namespace EcSiteBackend.Application.Common.Mappings
{
    /// <summary>
    /// ユーザーオブジェクトのマッピング設定
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User → UserDto
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.DisplayName));


        }
    }
}
