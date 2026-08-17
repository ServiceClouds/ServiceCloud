using Application.Abstractions.Queries;
using Application.Abstractions.Repositories;
using Application.Common;
using Shared.Response;

namespace Application.Features.TenantFeatures.Services.Queries.GetPagedServices;

public sealed class GetPagedServicesQueryHandler
    : IQueryHandler<
        GetPagedServicesQuery,
        PagedResponse<GetPagedServicesResponse>>
{
    private readonly IServiceRepository _repository;

    public GetPagedServicesQueryHandler(
        IServiceRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<PagedResponse<GetPagedServicesResponse>>> Handle(
        GetPagedServicesQuery request,
        CancellationToken cancellationToken)
    {
        // ============================================================
        // 1. Get paginated services from the tenant repository
        // ============================================================

        var result = await _repository.GetPagedAsync(
            request.Pagination,
            cancellationToken);

        if (result.IsFailure)
        {
            return Result<PagedResponse<GetPagedServicesResponse>>
                .Failure(result.Error);
        }

        // ============================================================
        // 2. Map domain entities to query response DTOs
        // ============================================================

        var response = new PagedResponse<GetPagedServicesResponse>
        {
            Items = result.Value.Items
                .Select(service => new GetPagedServicesResponse(
                    service.ServiceId,
                    service.ServiceName,
                    service.Description))
                .ToList(),

            PageNumber = result.Value.PageNumber,
            PageSize = result.Value.PageSize,
            TotalRecords = result.Value.TotalRecords,
            TotalPages = result.Value.TotalPages
        };

        // ============================================================
        // 3. Return successful response
        // ============================================================

        return Result<PagedResponse<GetPagedServicesResponse>>
            .Success(response);
    }
}