using Domain.Entities.ServiceCloud;
using MediatR;
using Shared.Response;

namespace Application.Features.Masterfeatures.Staffs.Queries.GetAllStaff;

public sealed record GetAllStaffQuery
    : IRequest<Result<List<Staff>>>;