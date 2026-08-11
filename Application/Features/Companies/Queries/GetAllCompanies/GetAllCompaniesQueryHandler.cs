using Domain.Entities.ServiceCloud;
using MediatR;
using Persistence.Repositories.Common;
using Shared.Response;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Companies.Queries.GetAllCompanies;

public sealed class GetAllCompaniesQueryHandler
    : IRequestHandler<GetAllCompaniesQuery, Result<List<Company>>>
{
    private readonly IGenericRepository<Company> _companyRepository;

    public GetAllCompaniesQueryHandler(
        IGenericRepository<Company> companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task<Result<List<Company>>> Handle(
        GetAllCompaniesQuery request,
        CancellationToken cancellationToken)
    {
        var companies = await _companyRepository
            .GetAll()
            .Where(x => !x.IsArchived)
            .ToListAsync(cancellationToken);

        return Result<List<Company>>.Success(companies);
    }
}