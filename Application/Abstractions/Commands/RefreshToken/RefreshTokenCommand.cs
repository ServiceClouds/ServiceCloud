using Application.Abstractions.Commands.Login;

namespace Application.Abstractions.Commands.Login.RefreshToken;

public sealed record RefreshTokenCommand(
    string RefreshToken)
    : ICommand<LoginResponse>;