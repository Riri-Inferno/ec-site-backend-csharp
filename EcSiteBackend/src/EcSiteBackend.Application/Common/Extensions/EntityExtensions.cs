using EcSiteBackend.Domain.Entities;
using System.Reflection;

namespace EcSiteBackend.Application.Common.Extensions
{
    /// <summary>
    /// エンティティの拡張メソッド
    /// </summary>
    public static class EntityExtensions
    {
        public static T CloneForHistory<T>(this T entity) where T : BaseEntity, new()
        {
            var clone = new T();
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead && p.CanWrite);

            foreach (var property in properties)
            {
                // ナビゲーションプロパティはスキップ
                if (property.PropertyType.IsClass &&
                    property.PropertyType != typeof(string) &&
                    typeof(BaseEntity).IsAssignableFrom(property.PropertyType))
                {
                    continue;
                }

                // コレクションもスキップ
                if (property.PropertyType.IsGenericType &&
                    property.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>))
                {
                    continue;
                }

                var value = property.GetValue(entity);
                property.SetValue(clone, value);
            }

            return clone;
        }
    }
}
