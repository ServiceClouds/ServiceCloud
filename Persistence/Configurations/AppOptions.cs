namespace Persistence.Configurations
{
    public class AppOptions
    {
        public ConnectionStringsOptions ConnectionStrings { get; init; } = new();
        public BearerTokensOptions BearerTokens { get; init; } = new();
        public AppSettingOptions AppSetting { get; init; } = new();
    }

    public sealed class ConnectionStringsOptions
    {
        public string MasterDatabase { get; init; } = default!;
    }



    public sealed class BearerTokensOptions
    {
        public string Key { get; init; } = default!;
        public string Issuer { get; init; } = default!;
        public string Audience { get; init; } = default!;
        public int AccessTokenExpirationInHours { get; init; }
        public int RefreshTokenExpirationMinutes { get; init; }
    }

    public sealed class AppSettingOptions
    {
        public bool IsProduction { get; init; }
    }
}