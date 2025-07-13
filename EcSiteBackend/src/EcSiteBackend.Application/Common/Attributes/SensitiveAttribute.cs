namespace EcSiteBackend.Application.Common.Attributes
{
    /// <summary>
    /// 機密情報を示す属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class SensitiveAttribute : Attribute
    {
        public string? Reason { get; }

        public SensitiveAttribute(string? reason = null)
        {
            Reason = reason;
        }
    }
}
