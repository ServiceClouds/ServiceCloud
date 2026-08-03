using Application.Abstractions.Authentication;
using Application.Abstractions.Commands.Login;
using Application.Abstractions.Repositories;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;


namespace Application.Abstractions.Commands.Login.VefityLogin
{
    public sealed class VerifyLoginHandler
    : ICommandHandler<VerifyLoginCommand, VerifyLoginResponse>
    {
        private readonly IAuthRepository _authRepository;
        private readonly IPasswordHasher _passwordHasher;

        public VerifyLoginHandler(
            IAuthRepository authRepository,
            IPasswordHasher passwordHasher)
        {
            _authRepository = authRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<Result<VerifyLoginResponse>> Handle(
            VerifyLoginCommand command,
            CancellationToken cancellationToken)
        {
            // 1. Get Staff
            var staff = await _authRepository.GetStaffAsync(
                command.StaffId,
                cancellationToken);

            if (staff is null)
            {
                return Result<VerifyLoginResponse>.Failure(
                    Error.NotFound("Authentication.InvalidCredentials Invalid email or password."));
            }

            // 2. Validate Company
            if (staff.CompanyId != command.CompanyId)
            {
                return Result<VerifyLoginResponse>.Failure(
                    Error.NotFound("Authentication.InvalidCredentials Invalid email or password."));
            }

            // 3. Get Login Information
            var staffLogin = await _authRepository.GetStaffLoginAsync(
                command.StaffId,
                command.CompanyId,
                cancellationToken);

            if (staffLogin is null)
            {
                return Result<VerifyLoginResponse>.Failure(
                    Error.NotFound("Authentication.InvalidCredentials Invalid email or password."));
            }

            // 4. Verify Password
            var isPasswordValid = _passwordHasher.VerifyPassword(
                command.Password,
                staffLogin.PasswordHash!,
                staffLogin.Salt!);

            if (!isPasswordValid)
            {
                return Result<VerifyLoginResponse>.Failure(
                    Error.NotFound("Authentication.InvalidCredentials Invalid email or password."));
            }

            // 5. Check Allow Login
            if (!staff.AllowLogin)
            {
                return Result<VerifyLoginResponse>.Failure(
                    Error.NotFound("Authentication.InvalidCredentials Invalid email or password."));
            }

            // 6. Check Suspension
            if (staffLogin.IsSuspended)
            {
                return Result<VerifyLoginResponse>.Failure(
                    Error.NotFound("Authentication.InvalidCredentials Invalid email or password."));
            }

            // 7. Get Branches
            var branches = await _authRepository.GetBranchesAsync(
                command.StaffId,
                command.CompanyId,
                cancellationToken);

            
            return Result<VerifyLoginResponse>.Success(
                new VerifyLoginResponse
                {
                    Branches = branches
                });
        }
    }





}
