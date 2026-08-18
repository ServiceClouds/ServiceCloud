using Application.Abstractions.Repositories.Common;
using Domain.Entities.ServiceCloud;
using MediatR;
using Microsoft.EntityFrameworkCore;

using Shared.Response;

namespace Application.Features.Masterfeatures.Staffs.Queries.GetAllStaff;

public sealed class GetAllStaffQueryHandler
    : IRequestHandler<GetAllStaffQuery, Result<List<Staff>>>
{
    private readonly IMasterRepository<Staff> _staffRepository;

    public GetAllStaffQueryHandler(
        IMasterRepository<Staff> staffRepository)
    {
        _staffRepository = staffRepository;
    }

    public async Task<Result<List<Staff>>> Handle(
        GetAllStaffQuery request,
        CancellationToken cancellationToken)
    {
        var staff = await _staffRepository
            .GetAll()
            .ToListAsync(cancellationToken);

        return Result<List<Staff>>.Success(staff);
    }
}