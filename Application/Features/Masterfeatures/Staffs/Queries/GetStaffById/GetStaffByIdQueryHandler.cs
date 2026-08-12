using Domain.Entities.ServiceCloud;
using MediatR;
using Persistence.Repositories.Common;
using Shared.Response;

namespace Application.Features.Masterfeatures.Staffs.Queries.GetStaffById;

public sealed class GetStaffByIdQueryHandler
    : IRequestHandler<GetStaffByIdQuery, Result<Staff>>
{
    private readonly IGenericRepository<Staff> _staffRepository;

    public GetStaffByIdQueryHandler(
        IGenericRepository<Staff> staffRepository)
    {
        _staffRepository = staffRepository;
    }

    public async Task<Result<Staff>> Handle(
        GetStaffByIdQuery request,
        CancellationToken cancellationToken)
    {
        var staff = await _staffRepository.FirstOrDefaultAsync(
            x => x.StaffId == request.StaffId,
            cancellationToken: cancellationToken);

        if (staff is null)
        {
            return Result<Staff>.Failure(
                Error.NotFound("Staff not found."));
        }

        return Result<Staff>.Success(staff);
    }
}