using Application.Abstractions.Queries;
using Application.Abstractions.Repositories;
using Shared.Response;

namespace Application.Features.Services.Queries.GetServiceById;

public sealed class GetServiceByIdQueryHandler
    : IQueryHandler<GetServiceByIdQuery, GetServiceByIdResponse>
{
    private readonly IServiceRepository _repository;

    public GetServiceByIdQueryHandler(IServiceRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<GetServiceByIdResponse>> Handle(
        GetServiceByIdQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _repository.GetByIdAsync(
            request.CompanyId,
            request.ServiceId,
            cancellationToken);

        if (result.IsFailure)
        {
            return Result<GetServiceByIdResponse>.Failure(result.Error);
        }

        var service = result.Value!;

        var response = new GetServiceByIdResponse(
            service.ServiceId,
            service.ServiceCategoryId,
            service.ServiceName,
            service.Description,
            service.SpecialInstruction,
            service.HasBranchPermission,
            service.AllowBranchEditPrice
        );

        return Result<GetServiceByIdResponse>.Success(response);
    }
}