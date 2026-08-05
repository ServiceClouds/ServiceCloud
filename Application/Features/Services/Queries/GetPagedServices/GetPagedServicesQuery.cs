using Application.Abstractions.Queries;
using Application.Common;

namespace Application.Features.Services.Queries.GetPagedServices;

public sealed record GetPagedServicesQuery(
    
    PaginationRequest Pagination
) : IQuery<PagedResponse<GetPagedServicesResponse>>;