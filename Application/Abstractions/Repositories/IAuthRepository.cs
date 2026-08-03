using Application.Abstractions.Authentication;
using Application.Abstractions.Commands.Login.GetCompanies;
using Application.Abstractions.Commands.Login.VefityLogin;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Abstractions.Repositories
{
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


    }


    }
