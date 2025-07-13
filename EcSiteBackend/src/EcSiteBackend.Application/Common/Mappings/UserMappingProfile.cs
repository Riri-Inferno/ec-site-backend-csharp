using AutoMapper;
using EcSiteBackend.Application.DTOs;
using EcSiteBackend.Application.UseCases.InputOutputModels;
using EcSiteBackend.Domain.Entities;

namespace EcSiteBackend.Application.Common.Mappings
{
    /// <summary>
    /// ユーザーオブジェクトのマッピング設定
    /// </summary>
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            // User ↔ UserDto
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.DisplayName))
                .ReverseMap();

            // User → AuthOutput
            CreateMap<User, AuthOutput>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.Token, opt => opt.Ignore())
                .ForMember(dest => dest.ExpiresAt, opt => opt.Ignore())
                .ReverseMap();

            // SignUpInput → User（新規作成用）
            CreateMap<SignUpInput, User>()
                .IgnoreBaseEntityProperties()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true))
                .ForMember(dest => dest.EmailConfirmed, opt => opt.MapFrom(_ => false))
                .ForMember(dest => dest.LastLoginAt, opt => opt.Ignore())
                .IgnoreAllNavigationProperties();

            // UpdateUserInput → User（更新用）
            CreateMap<UpdateUserInput, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .IgnoreAllNavigationProperties();

            // User → UserHistory（履歴保存用）
            CreateMap<User, UserHistory>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.OriginalId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.OperatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.OperationType, opt => opt.Ignore()) // 呼び出し時に設定
                .ForMember(dest => dest.OperatedBy, opt => opt.Ignore()) // 呼び出し時に設定
                .ForMember(dest => dest.OriginalUser, opt => opt.Ignore());
        }
    }
}
