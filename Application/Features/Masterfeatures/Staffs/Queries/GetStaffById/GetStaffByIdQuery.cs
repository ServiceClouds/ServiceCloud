using Domain.Entities.ServiceCloud;
using MediatR;
using Shared.Response;

namespace Application.Features.Masterfeatures.Staffs.Queries.GetStaffById;

public sealed record GetStaffByIdQuery(
    int StaffId
) : IRequest<Result<Staff>>;