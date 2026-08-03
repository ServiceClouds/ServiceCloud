using Application.Abstractions.Authentication;
using Application.Abstractions.Commands.Login.GetCompanies;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Abstractions.Repositories
{
    public interface IAuthRepository
    {
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
    }



}
