using Application.Abstractions.Queries;

namespace Application.Features.Services.Queries.GetServiceById;

public sealed record GetServiceByIdQuery(
    int CompanyId,
    int ServiceId
) : IQuery<GetServiceByIdResponse>;