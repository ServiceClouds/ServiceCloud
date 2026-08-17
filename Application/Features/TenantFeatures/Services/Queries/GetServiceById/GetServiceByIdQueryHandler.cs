using Application.Abstractions.Queries;
using Application.Abstractions.Repositories;
using Shared.Response;

namespace Application.Features.TenantFeatures.Services.Queries.GetServiceById;

public sealed class GetServiceByIdQueryHandler
    : IQueryHandler<GetServiceByIdQuery, GetServiceByIdResponse>
{
    private readonly IServiceRepository _repository;

    public GetServiceByIdQueryHandler(
        IServiceRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<GetServiceByIdResponse>> Handle(
        GetServiceByIdQuery request,
        CancellationToken cancellationToken)
    {
        // ============================================================
        // 1. Get the active service
        // ============================================================

        var service = await _repository.FirstOrDefaultAsync(
            x =>
                x.ServiceId == request.ServiceId &&
                !x.IsArchived,
            cancellationToken: cancellationToken);

        // ============================================================
        // 2. Service not found
        // ============================================================

        if (service is null)
        {
            return Result<GetServiceByIdResponse>.Failure(
                Error.NotFound("Service not found."));
        }

        // ============================================================
        // 3. Map entity to response
        // ============================================================

        var response = new GetServiceByIdResponse(
            service.ServiceId,
            service.ServiceCategoryId,
            service.ServiceName,
            service.Description,
            service.SpecialInstruction,
            service.HasBranchPermission,
            service.AllowBranchEditPrice
        );

        // ============================================================
        // 4. Return successful result
        // ============================================================

        return Result<GetServiceByIdResponse>.Success(response);
    }
}