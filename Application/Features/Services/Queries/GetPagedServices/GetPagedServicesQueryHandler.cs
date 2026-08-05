using Application.Abstractions.Queries;
using Application.Abstractions.Repositories;
using Application.Common;
using Shared.Response;

namespace Application.Features.Services.Queries.GetPagedServices;

public sealed class GetPagedServicesQueryHandler
    : IQueryHandler<GetPagedServicesQuery, PagedResponse<GetPagedServicesResponse>>
{
    private readonly IServiceRepository _repository;

    public GetPagedServicesQueryHandler(IServiceRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<PagedResponse<GetPagedServicesResponse>>> Handle(
        GetPagedServicesQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _repository.GetPagedAsync(
      
            request.Pagination,
            cancellationToken);

        if (result.IsFailure)
        {
            return Result<PagedResponse<GetPagedServicesResponse>>
                .Failure(result.Error);
        }

        var response = new PagedResponse<GetPagedServicesResponse>
        {
            Items = result.Value.Items
                .Select(x => new GetPagedServicesResponse(
                    x.ServiceId,
                    x.ServiceName,
                    x.Description))
                .ToList(),

            PageNumber = result.Value.PageNumber,

            PageSize = result.Value.PageSize,

            TotalRecords = result.Value.TotalRecords,

            TotalPages = result.Value.TotalPages
        };

        return Result<PagedResponse<GetPagedServicesResponse>>
            .Success(response);
    }
}