namespace Application.Abstractions.Commands.Login;

public sealed class LoginResponse
{
    public string AccessToken { get; init; } = string.Empty;

    public string RefreshToken { get; init; } = string.Empty;

    public DateTime ExpiresAt { get; init; }

    public DateTime RefreshTokenExpiresAt { get; init; }

    public string TokenType => "Bearer";
}