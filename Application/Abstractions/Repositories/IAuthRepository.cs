using Application.Abstractions.Commands.Login.GetCompanies;
using Application.Abstractions.Commands.Login.RefreshToken;
using Application.Abstractions.Commands.Login.VefityLogin;
using Domain.Entities.ServiceCloud;

namespace Application.Abstractions.Repositories;

public interface IAuthRepository
{
    Task<Staff?> GetStaffAsync(
        int staffId,
        CancellationToken cancellationToken = default);

    Task<Staff?> GetStaffByEmailAsync(
        string email,
        CancellationToken cancellationToken = default);

    Task<StaffLogin?> GetStaffLoginAsync(
        int staffId,
        int companyId,
        CancellationToken cancellationToken = default);

    Task<Company?> GetCompanyAsync(
        int companyId,
        CancellationToken cancellationToken = default);

    Task<List<CompanyLookupResponse>> GetCompaniesByEmailAsync(
        string email,
        CancellationToken cancellationToken);

    Task<List<BranchLookupResponse>> GetBranchesAsync(
        int staffId,
        int companyId,
        CancellationToken cancellationToken = default);

    Task<bool> IsBranchAssignedAsync(
        int staffId,
        int companyId,
        int branchId,
        CancellationToken cancellationToken = default);

    Task AddStaffTokenAsync(
        StaffToken staffToken,
        CancellationToken cancellationToken = default);

    Task AddLoggedInBranchAsync(
        StaffLoggedInBranch loggedInBranch,
        CancellationToken cancellationToken = default);

    Task<RefreshTokenData?> GetRefreshTokenDataAsync(
        string refreshToken,
        CancellationToken cancellationToken = default);

    Task<StaffToken?> GetStaffTokenAsync(
        long staffTokenId,
        CancellationToken cancellationToken = default);

    Task UpdateStaffTokenAsync(
        StaffToken staffToken,
        CancellationToken cancellationToken = default);
}