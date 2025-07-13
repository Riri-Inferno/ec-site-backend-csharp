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

        /// <summary>
        /// GraphQLクエリ内のセンシティブなフィールドをマスクする
        /// </summary>
        public static string MaskGraphQLQuery(string query, Assembly targetAssembly)
        {
            var maskedQuery = query;

            // アセンブリ内のすべての型を検査
            var inputTypes = targetAssembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("InputType"))
                .ToList();

            foreach (var type in inputTypes)
            {
                foreach (var prop in type.GetProperties())
                {
                    if (prop.GetCustomAttribute<SensitiveAttribute>() != null)
                    {
                        // GraphQLの慣例に従ってフィールド名を小文字化
                        var fieldName = char.ToLower(prop.Name[0]) + prop.Name.Substring(1);

                        // 正規表現でフィールドの値をマスク
                        var patterns = new[]
                        {
                            $@"{fieldName}:\s*""[^""]*""",  // 文字列値
                            $@"{fieldName}:\s*'[^']*'",      // シングルクォート
                            $@"{fieldName}:\s*[^\s,}}]+",    // クォートなしの値
                        };

                        foreach (var pattern in patterns)
                        {
                            maskedQuery = System.Text.RegularExpressions.Regex.Replace(
                                maskedQuery,
                                pattern,
                                m =>
                                {
                                    var match = m.Value;
                                    var colonIndex = match.IndexOf(':');
                                    return match.Substring(0, colonIndex + 1) + " \"***\"";
                                },
                                System.Text.RegularExpressions.RegexOptions.IgnoreCase
                            );
                        }
                    }
                }
            }

            return maskedQuery;
        }
    }
}
