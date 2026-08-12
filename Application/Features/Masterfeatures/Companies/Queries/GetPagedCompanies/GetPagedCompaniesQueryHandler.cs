using Application.Common;
using Domain.Entities.ServiceCloud;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories.Common;
using Shared.Response;

namespace Application.Features.Masterfeatures.Companies.Queries.GetPagedCompanies;

public sealed class GetPagedCompaniesQueryHandler
    : IRequestHandler<
        GetPagedCompaniesQuery,
        Result<PagedResponse<Company>>>
{
    private readonly IGenericRepository<Company> _companyRepository;

    public GetPagedCompaniesQueryHandler(
        IGenericRepository<Company> companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task<Result<PagedResponse<Company>>> Handle(
        GetPagedCompaniesQuery request,
        CancellationToken cancellationToken)
    {
        IQueryable<Company> query = _companyRepository
            .GetAll()
            .Where(x => !x.IsArchived);

        // Search
        if (!string.IsNullOrWhiteSpace(request.Request.Search))
        {
            query = query.Where(x =>
                x.CompanyCode.Contains(request.Request.Search) ||
                x.CompanyName.Contains(request.Request.Search));
        }

        // Total records BEFORE pagination
        var totalRecords = await query.CountAsync(
            cancellationToken);

        // Pagination
        var companies = await query
            .Skip(
                (request.Request.PageNumber - 1)
                * request.Request.PageSize)
            .Take(request.Request.PageSize)
            .ToListAsync(cancellationToken);

        var response = new PagedResponse<Company>
        {
            Items = companies,
            PageNumber = request.Request.PageNumber,
            PageSize = request.Request.PageSize,
            TotalRecords = totalRecords,
            TotalPages =
                (int)Math.Ceiling(
                    (double)totalRecords /
                    request.Request.PageSize)
        };

        return Result<PagedResponse<Company>>.Success(response);
    }
}