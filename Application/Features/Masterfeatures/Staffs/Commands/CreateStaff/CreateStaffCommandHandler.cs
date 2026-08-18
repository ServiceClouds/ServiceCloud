using Application.Common;
using Domain.Entities.ServiceCloud;
using MediatR;
using Application.Abstractions.Repositories.Common;
using Shared.Response;

namespace Application.Features.Masterfeatures.Staffs.Commands.CreateStaff;

public sealed class CreateStaffCommandHandler
    : IRequestHandler<CreateStaffCommand, Result<int>>
{
    private readonly IMasterRepository<Staff> _staffRepository;
    private readonly IMasterUnitOfWork _unitOfWork;

    public CreateStaffCommandHandler(
        IMasterRepository<Staff> staffRepository,
        IMasterUnitOfWork unitOfWork)
    {
        _staffRepository = staffRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Handle(
        CreateStaffCommand request,
        CancellationToken cancellationToken)
    {
        var exists = await _staffRepository.ExistsAsync(
            x => x.Email == request.Email,
            cancellationToken);

        if (exists)
        {
            return Result<int>.Failure(
                Error.Conflict(
                    "Staff with this email already exists."));
        }

        var staff = Staff.Create(
            staffPositionId: request.StaffPositionId,
            companyId: request.CompanyId,
            firstName: request.FirstName,
            email: request.Email,
            createdBy: 1,

            countryId: request.CountryId,
            stateCountyId: request.StateCountyId,
            enterpriseRoleId: request.EnterpriseRoleId,
            title: request.Title,
            lastName: request.LastName,
            cardNumber: request.CardNumber,
            gender: request.Gender,
            birthDate: request.BirthDate,
            phone: request.Phone,
            mobile: request.Mobile,
            address1: request.Address1,
            address2: request.Address2,
            cityName: request.CityName,
            stateCountyName: request.StateCountyName,
            postCode: request.PostCode,
            joiningDate: request.JoiningDate,
            employmentTypeId: request.EmploymentTypeId,
            probationValue: request.ProbationValue,
            probationDurationTypeId: request.ProbationDurationTypeId,
            probationMonths: request.ProbationMonths,
            employmentType: request.EmploymentType,
            isSuperAdmin: request.IsSuperAdmin,
            allowLogin: request.AllowLogin,
            imagePath: request.ImagePath,
            notes: request.Notes
        );

        _staffRepository.Add(staff);

        var saveResult = await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        if (saveResult.IsFailure)
        {
            return Result<int>.Failure(saveResult.Error);
        }

        return Result<int>.Success(staff.StaffId);
    }
}