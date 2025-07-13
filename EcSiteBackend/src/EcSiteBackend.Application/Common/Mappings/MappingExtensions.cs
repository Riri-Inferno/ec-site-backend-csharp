using AutoMapper;
using EcSiteBackend.Domain.Entities;

namespace EcSiteBackend.Application.Common.Mappings
{
    public static class MappingExtensions
    {
        /// <summary>
        /// ナビゲーションプロパティを無視する拡張メソッド
        /// </summary>
        public static IMappingExpression<TSource, TDestination> IgnoreAllNavigationProperties<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> mappingExpression)
            where TDestination : BaseEntity
        {
            var destinationType = typeof(TDestination);
            var navigationProperties = destinationType.GetProperties()
                .Where(p => 
                {
                    // コレクション型
                    if (p.PropertyType.IsGenericType &&
                        (p.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>) ||
                        p.PropertyType.GetGenericTypeDefinition() == typeof(IList<>) ||
                        p.PropertyType.GetGenericTypeDefinition() == typeof(List<>)))
                        return true;
                        
                    // エンティティ型（BaseEntityを継承している型）のみを対象
                    if (p.PropertyType.IsClass && 
                        p.PropertyType != typeof(string) &&
                        typeof(BaseEntity).IsAssignableFrom(p.PropertyType))
                        return true;
                        
                    return false;
                })
                .Select(p => p.Name);

            foreach (var property in navigationProperties)
            {
                mappingExpression.ForMember(property, opt => opt.Ignore());
            }

            return mappingExpression;
        }

        /// <summary>
        /// BaseEntityの監査フィールドを無視する拡張メソッド
        /// </summary>
        public static IMappingExpression<TSource, TDestination> IgnoreBaseEntityProperties<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> mappingExpression)
            where TDestination : BaseEntity
        {
            mappingExpression
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedBy, opt => opt.Ignore());

            return mappingExpression;
        }
    }
}
