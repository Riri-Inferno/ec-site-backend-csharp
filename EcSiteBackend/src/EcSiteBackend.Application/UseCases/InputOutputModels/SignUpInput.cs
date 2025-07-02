namespace EcSiteBackend.Application.UseCases.InputOutputModels
{
    /// <summary>
    /// SignUp時の入力モデル
    /// </summary>
    public class SignUpInput
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
    }
}
