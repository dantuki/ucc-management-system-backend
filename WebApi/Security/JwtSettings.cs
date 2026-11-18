namespace WebApi.Security;

public sealed class JwtSettings
{
    public const string SectionName = "Jwt";
    public string Issuer { get; init; } = "siga-ucc";
    public string Audience { get; init; } = "siga-ucc-api";
    public string SigningKey { get; set; } = string.Empty;
    public int ExpirationMinutes { get; init; } = 30;
}
