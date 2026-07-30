using Application.Abstractions.Authentication;
using Application.Abstractions.Commands;
using Application.Abstractions.Commands.Login;
using Application.Abstractions.Repositories;


using Shared.Response;

namespace Application.Commands.Login;

public sealed class LoginCommandHandler: ICommandHandler<LoginCommand, LoginResponse>
{
    private readonly IAuthRepository _authRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginCommandHandler(
        IAuthRepository authRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _authRepository = authRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<Result<LoginResponse>> Handle(
        LoginCommand command,
        CancellationToken cancellationToken)
    {
        // 1. Find Staff
        var staff = await _authRepository.GetStaffByEmailAsync(
            command.Email,//verify exists
            cancellationToken);

        if (staff is null)
        {
            return Result<LoginResponse>.Failure(
                Error.NotFound("Authentication.InvalidCredentials Invalid email or password."));
        }

        // 2. Get Login Information
        var staffLogin = await _authRepository.GetStaffLoginAsync(//
            staff.StaffId,
            cancellationToken);

        if (staffLogin is null)
        {
            return Result<LoginResponse>.Failure(
                Error.NotFound("Authentication.InvalidCredentials Invalid email or password."));
        }

        // 3. Verify Password
        var isPasswordValid = _passwordHasher.VerifyPassword(
            command.Password,
            staffLogin.PasswordHash!,
            staffLogin.Salt!);

        if (!isPasswordValid)
        {
            return Result<LoginResponse>.Failure(
                Error.NotFound("Authentication.InvalidCredentials Invalid email or password."));
        }

        // 4. Check Login Permission
        if (!staff.AllowLogin)
        {
            return Result<LoginResponse>.Failure(
                Error.NotFound("Authentication.InvalidCredentials Invalid email or password."));
        }

        // 5. Check Suspension
        if (staffLogin.IsSuspended)
        {
            return Result<LoginResponse>.Failure(
                Error.NotFound("Authentication.InvalidCredentials Invalid email or password."));
        }

        // 6. Get Company
        var company = await _authRepository.GetCompanyAsync(
            staff.CompanyId,
            cancellationToken);//for later we need

        if (company is null)
        {
            return Result<LoginResponse>.Failure(
                Error.NotFound("Authentication.InvalidCredentials Invalid email or password."));
        }

        // 7. Create Authenticated User
        var authenticatedUser = new AuthenticatedUser
        {
            StaffId = staff.StaffId,
            CompanyId = company.CompanyId,
            Email = staff.Email,
            IsSuperAdmin = staff.IsSuperAdmin,
            HasEnterpriseRole = staffLogin.HasEnterpriseRole
        };

        // 8. Generate JWT
        var response = _jwtTokenGenerator.GenerateToken(authenticatedUser);

        return Result<LoginResponse>.Success(response);
    }
}
