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
