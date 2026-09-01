//using Application.Abstractions.Commands;
//using Application.Abstractions.Data;
//using Application.Abstractions.Repositories.Common;
//using Application.Common;
//using Domain.Tenant.ServiceCloudTenant.Entities;
//using Shared.Response;

//namespace Application.Features.TenantFeatures.Staff.Commands.ArchiveStaff;

//public sealed class ArchiveStaffCommandHandler
//    : ICommandHandler<ArchiveStaffCommand>
//{
//    private readonly ITenantRepository<Domain.Tenant.ServiceCloudTenant.Entities.Staff> _repository;
//    private readonly IUnitOfWork _unitOfWork;
//    private readonly IUserContext _userContext;

//    public ArchiveStaffCommandHandler(
//        ITenantRepository<Domain.Tenant.ServiceCloudTenant.Entities.Staff> repository,
//        IUnitOfWork unitOfWork,
//        IUserContext userContext)
//    {
//        _repository = repository;
//        _unitOfWork = unitOfWork;
//        _userContext = userContext;
//    }

//    public async Task<Result> Handle(
//        ArchiveStaffCommand request,
//        CancellationToken cancellationToken)
//    {
//        var staff =
//            await _repository.FirstOrDefaultAsync(
//                x =>
//                    x.StaffId == request.StaffId &&
//                    !x.IsArchived,
//                asNoTracking: false,
//                cancellationToken);

//        if (staff is null)
//        {
//            return Result.Failure(
//                Error.NotFound("Staff not found."));
//        }

//        staff.Archive(_userContext.StaffId);

//        _repository.Update(staff);

//        var saveResult =
//            await _unitOfWork.SaveChangesAsync(
//                cancellationToken);

//        if (saveResult.IsFailure)
//        {
//            return Result.Failure(saveResult.Error);
//        }

//        return Result.Success();
//    }
//}