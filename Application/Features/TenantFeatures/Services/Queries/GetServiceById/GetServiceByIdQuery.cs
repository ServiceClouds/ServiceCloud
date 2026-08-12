using Application.Abstractions.Queries;

namespace Application.Features.TenantFeatures.Services.Queries.GetServiceById;

public sealed record GetServiceByIdQuery(
    
    int ServiceId
) : IQuery<GetServiceByIdResponse>;