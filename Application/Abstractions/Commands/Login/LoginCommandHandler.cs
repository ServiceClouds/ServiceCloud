using Application.Abstractions.Authentication;
using Application.Abstractions.Commands;
using Application.Abstractions.Commands.Login;
using Application.Abstractions.Repositories;
using Domain.Entities.ServiceCloud;
using Shared.Response;

namespace Application.Commands.Login;

public sealed class LoginCommandHandler
    : ICommandHandler<LoginCommand, LoginResponse>
{
    private readonly IAuthRepository _authRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginCommandHandler(
        IAuthRepository authRepository,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _authRepository = authRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<Result<LoginResponse>> Handle(
        LoginCommand command,
        CancellationToken cancellationToken)
    {
        // 1. Get Staff
        var staff = await _authRepository.GetStaffAsync(
            command.StaffId,
            cancellationToken);

        if (staff is null)
        {
            return Result<LoginResponse>.Failure(
                Error.NotFound("Authentication.InvalidCredentials Invalid login."));
        }

        // 2. Validate Company
        if (staff.CompanyId != command.CompanyId)
        {
            return Result<LoginResponse>.Failure(
                Error.NotFound("Authentication.InvalidCredentials Invalid login."));
        }

        // 3. Get Staff Login
        var staffLogin = await _authRepository.GetStaffLoginAsync(
            command.StaffId,
            command.CompanyId,
            cancellationToken);

        if (staffLogin is null)
        {
            return Result<LoginResponse>.Failure(
                Error.NotFound("Authentication.InvalidCredentials Invalid login."));
        }

        // 4. Validate Branch Assignment
        var isBranchAssigned = await _authRepository.IsBranchAssignedAsync(
            command.StaffId,
            command.CompanyId,
            command.BranchId,
            cancellationToken);

        if (!isBranchAssigned)
        {
            return Result<LoginResponse>.Failure(
                Error.Unauthorized("Authentication.InvalidBranch Selected branch is not assigned."));
        }

        // 5. Create Authenticated User
        var authenticatedUser = new AuthenticatedUser
        {
            StaffId = staff.StaffId,
            CompanyId = staff.CompanyId,
            BranchId = command.BranchId,
            Email = staff.Email,
            IsSuperAdmin = staff.IsSuperAdmin,
            HasEnterpriseRole = staffLogin.HasEnterpriseRole
        };

        // 6. Generate JWT
        var response = _jwtTokenGenerator.GenerateToken(authenticatedUser);

        // 7. Save Staff Token
        var staffToken = StaffToken.Create(
      staffLogin.StaffLoginId,
      response.AccessToken,
      response.ExpiresAt,
      response.RefreshToken,
      response.RefreshTokenExpiresAt);

        await _authRepository.AddStaffTokenAsync(
            staffToken,
            cancellationToken);

        // 8. Save Logged In Branch
        var loggedInBranch = StaffLoggedInBranch.Create(
    staff.StaffId,
    command.BranchId);

        await _authRepository.AddLoggedInBranchAsync(
            loggedInBranch,
            cancellationToken);

        // 9. Return JWT
        return Result<LoginResponse>.Success(response);
    }
}