using System.Reflection;
using System.Text.Json;
using EcSiteBackend.Application.Common.Attributes;

namespace EcSiteBackend.Presentation.EcSiteBackend.WebAPI.Utils
{
    /// <summary>
    /// 機密情報のマスキングユーティリティ
    /// </summary>
    public static class MaskingUtil
    {
        /// <summary>
        /// オブジェクトの機密情報をマスクする
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static object MaskSensitiveProperties(object input)
        {
            var type = input.GetType();
            var result = new Dictionary<string, object?>();

            foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var value = prop.GetValue(input);
                var isSensitive = prop.GetCustomAttribute<SensitiveAttribute>() != null;

                result[prop.Name] = isSensitive ? "***" : value;
            }

            return result;
        }

        public static string ToMaskedJson(object input)
        {
            var masked = MaskSensitiveProperties(input);
            return JsonSerializer.Serialize(masked);
        }
    }
}
