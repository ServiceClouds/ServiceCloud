using Application.Common;
using Domain.Entities.ServiceCloud;
using MediatR;
using Persistence.Repositories.Common;
using Shared.Response;

namespace Application.Features.Masterfeatures.Staffs.Commands.UpdateStaff;

public sealed class UpdateStaffCommandHandler
    : IRequestHandler<UpdateStaffCommand, Result>
{
    private readonly IGenericRepository<Staff> _staffRepository;
    private readonly IMasterUnitOfWork _unitOfWork;

    public UpdateStaffCommandHandler(
        IGenericRepository<Staff> staffRepository,
        IMasterUnitOfWork unitOfWork)
    {
        _staffRepository = staffRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        UpdateStaffCommand request,
        CancellationToken cancellationToken)
    {
        var staff = await _staffRepository.FirstOrDefaultAsync(
            x => x.StaffId == request.StaffId,
            asNoTracking: false,
            cancellationToken: cancellationToken);

        if (staff is null)
        {
            return Result.Failure(
                Error.NotFound("Staff not found."));
        }

        staff.UpdateProfile(
            firstName: request.FirstName,
            lastName: request.LastName,
            email: request.Email,
            phone: request.Phone,
            mobile: request.Mobile,
            address1: request.Address1,
            address2: request.Address2,
            cityName: request.CityName,
            stateCountyName: request.StateCountyName,
            postCode: request.PostCode,
            modifiedBy: 1);

        _staffRepository.Update(staff);

        var saveResult = await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        if (saveResult.IsFailure)
        {
            return Result.Failure(saveResult.Error);
        }

        return Result.Success();
    }
}