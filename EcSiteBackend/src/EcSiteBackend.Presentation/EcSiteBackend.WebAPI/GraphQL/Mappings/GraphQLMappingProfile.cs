using AutoMapper;
using EcSiteBackend.Application.DTOs;
using EcSiteBackend.Presentation.EcSiteBackend.WebAPI.GraphQL.Types;
using EcSiteBackend.Application.UseCases.InputOutputModels;
using EcSiteBackend.Presentation.EcSiteBackend.WebAPI.GraphQL.Types.Payloads;

namespace EcSiteBackend.Presentation.EcSiteBackend.WebAPI.GraphQL.Mappings
{
    /// <summary>
    /// GraphQLスキーマ、Dto間ののAutoMapperプロファイル
    /// </summary>
    public class GraphQLMappingProfile : Profile
    {
        public GraphQLMappingProfile()
        {
            // DTO → GraphQL型
            CreateMap<UserDto, UserType>();

            // Application層のOutput → GraphQL Payload
            CreateMap<AuthOutput, AuthPayload>()
                .ForMember(dest => dest.Success, opt => opt.MapFrom(_ => true))
                .ForMember(dest => dest.Errors, opt => opt.Ignore());
        }
    }
}
