using Application.Abstractions.Authentication;
using Application.Abstractions.Commands;
using Application.Abstractions.Commands.Login;
using Application.Abstractions.Commands.Login.RefreshToken;
using Application.Abstractions.Repositories;
using Domain.Entities.ServiceCloud;
using Shared.Response;

namespace Application.Commands.Login.RefreshToken;

public sealed class RefreshTokenCommandHandler
    : ICommandHandler<RefreshTokenCommand, LoginResponse>
{
    private readonly IAuthRepository _authRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public RefreshTokenCommandHandler(
        IAuthRepository authRepository,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _authRepository = authRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<Result<LoginResponse>> Handle(
        RefreshTokenCommand command,
        CancellationToken cancellationToken)
    {
        var data = await _authRepository.GetRefreshTokenDataAsync(
            command.RefreshToken,
            cancellationToken);

        if (data is null)
        {
            return Result<LoginResponse>.Failure(
                Error.Unauthorized(
                    "Authentication.InvalidRefreshToken Invalid refresh token."));
        }

        if (!data.RefreshTokenExpiry.HasValue ||
            data.RefreshTokenExpiry.Value <= DateTime.UtcNow)
        {
            return Result<LoginResponse>.Failure(
                Error.Unauthorized(
                    "Authentication.RefreshTokenExpired Refresh token has expired."));
        }

        var authenticatedUser = new AuthenticatedUser
        {
            StaffId = data.StaffId,
            CompanyId = data.CompanyId,
            BranchId = data.BranchId,
            Email = data.Email,
            IsSuperAdmin = data.IsSuperAdmin,
            HasEnterpriseRole = data.HasEnterpriseRole
        };

        var response = _jwtTokenGenerator.GenerateToken(
            authenticatedUser);

        var staffToken = await _authRepository.GetStaffTokenAsync(
            data.StaffTokenId,
            cancellationToken);

        if (staffToken is null)
        {
            return Result<LoginResponse>.Failure(
                Error.Unauthorized(
                    "Authentication.InvalidRefreshToken Invalid refresh token."));
        }

        staffToken.UpdateTokens(
            response.AccessToken,
            response.ExpiresAt,
            response.RefreshToken,
            response.RefreshTokenExpiresAt);

        await _authRepository.UpdateStaffTokenAsync(
            staffToken,
            cancellationToken);

        return Result<LoginResponse>.Success(response);
    }
}