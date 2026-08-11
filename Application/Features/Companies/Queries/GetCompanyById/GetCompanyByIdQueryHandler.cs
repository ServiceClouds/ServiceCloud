using MediatR;
using Persistence.Repositories.Common;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities.ServiceCloud;

namespace Application.Features.Companies.Queries.GetCompanyById
{
    public sealed class GetCompanyByIdQueryHandler
     : IRequestHandler<GetCompanyByIdQuery, Result<Company>>
    {
        private readonly IGenericRepository<Company> _companyRepository;

        public GetCompanyByIdQueryHandler(
            IGenericRepository<Company> companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<Result<Company>> Handle(
            GetCompanyByIdQuery request,
            CancellationToken cancellationToken)
        {
            var company = await _companyRepository.FirstOrDefaultAsync(
                x => x.CompanyId == request.CompanyId &&
                     !x.IsArchived,
                cancellationToken: cancellationToken);

            if (company is null)
            {
                return Result<Company>.Failure(
                    Error.NotFound("Company not found."));
            }

            return Result<Company>.Success(company);
        }
    }
}
