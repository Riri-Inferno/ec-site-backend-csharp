using AutoMapper;
using EcSiteBackend.Domain.Entities;

namespace EcSiteBackend.Application.Common.Mappings
{
    public static class MappingExtensions
    {
        /// <summary>
        /// BaseEntityの監査フィールドを自動設定する拡張メソッド
        /// </summary>
        public static IMappingExpression<TSource, TDestination> ConfigureAuditableEntity<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> mappingExpression,
            bool isUpdate = false)
            where TDestination : BaseEntity
        {
            if (isUpdate)
            {
                // 更新時の設定
                mappingExpression
                    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(9))))                    .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                    .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                    .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                    .ForMember(dest => dest.Id, opt => opt.Ignore());
            }
            else
            {
                // 新規作成時の設定
                mappingExpression
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
                    .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(9))))
                    .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                    .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                    .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                    .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(_ => false))
                    .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
                    .ForMember(dest => dest.DeletedBy, opt => opt.Ignore());
            }

            return mappingExpression;
        }

        /// <summary>
        /// ナビゲーションプロパティを無視する拡張メソッド
        /// </summary>
        public static IMappingExpression<TSource, TDestination> IgnoreAllNavigationProperties<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> mappingExpression)
            where TDestination : BaseEntity
        {
            var destinationType = typeof(TDestination);
            var navigationProperties = destinationType.GetProperties()
                .Where(p => p.PropertyType.IsGenericType &&
                           (p.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>) ||
                            p.PropertyType.GetGenericTypeDefinition() == typeof(IList<>)) ||
                           p.PropertyType.IsClass && p.PropertyType != typeof(string))
                .Select(p => p.Name);

            foreach (var property in navigationProperties)
            {
                mappingExpression.ForMember(property, opt => opt.Ignore());
            }

            return mappingExpression;
        }
    }
}
