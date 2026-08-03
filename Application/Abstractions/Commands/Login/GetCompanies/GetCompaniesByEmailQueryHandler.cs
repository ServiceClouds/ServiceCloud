using Application.Abstractions.Repositories;
using Application.Abstractions.Queries;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Abstractions.Commands.Login.GetCompanies
{
   

    public sealed class GetCompaniesByEmailQueryHandler
        : IQueryHandler<GetCompaniesByEmailQuery, List<CompanyLookupResponse>>
    {
        private readonly IAuthRepository _authRepository;

        public GetCompaniesByEmailQueryHandler(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<Result<List<CompanyLookupResponse>>> Handle(
     GetCompaniesByEmailQuery query,
     CancellationToken cancellationToken)
        {
            var companies = await _authRepository.GetCompaniesByEmailAsync(
                query.Email,
                cancellationToken);

            if (companies == null || !companies.Any())
            {
                return Result<List<CompanyLookupResponse>>.Failure(
                    Error.NotFound(
                        "Authentication.CompanyNotFound No company found for the provided email."));
            }

            return Result<List<CompanyLookupResponse>>.Success(companies);
        }
    }
}
