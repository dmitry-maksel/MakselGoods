namespace Identity.API.Infrastructure.Settings
{
    public class JwtSettings
    {
        public const string SectionKey = "Jwt";

        public string? Audience { get; set; }

        public string? Issuer { get; set; }
    }
}
