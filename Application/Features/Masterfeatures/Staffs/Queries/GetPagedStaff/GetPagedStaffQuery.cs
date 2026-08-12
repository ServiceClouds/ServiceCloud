using Application.Common;
using Domain.Entities.ServiceCloud;
using MediatR;
using Shared.Response;

namespace Application.Features.Masterfeatures.Staffs.Queries.GetPagedStaff;

public sealed record GetPagedStaffQuery(
    PaginationRequest Request
) : IRequest<Result<PagedResponse<Staff>>>;