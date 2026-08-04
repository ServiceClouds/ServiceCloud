using Application.Abstractions.Queries;
using Application.Common;

namespace Application.Features.Services.Queries.GetPagedServices;

public sealed record GetPagedServicesQuery(
    int CompanyId,
    PaginationRequest Pagination
) : IQuery<PagedResponse<GetPagedServicesResponse>>;