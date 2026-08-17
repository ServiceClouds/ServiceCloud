using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories;
using Application.Common;
using Shared.Response;

namespace Application.Features.TenantFeatures.Services.Commands.UpdateService;

public sealed class UpdateServiceCommandHandler
    : ICommandHandler<UpdateServiceCommand, UpdateServiceResponse>
{
    private readonly IServiceRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public UpdateServiceCommandHandler(
        IServiceRepository repository,
        IUnitOfWork unitOfWork,
        IUserContext userContext)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<Result<UpdateServiceResponse>> Handle(
        UpdateServiceCommand request,
        CancellationToken cancellationToken)
    {
        // ============================================================
        // 1. Get Existing Service
        // ============================================================

        var service = await _repository.FirstOrDefaultAsync(
            x =>
                x.ServiceId == request.ServiceId &&
                !x.IsArchived,
            cancellationToken: cancellationToken);

        if (service is null)
        {
            return Result<UpdateServiceResponse>.Failure(
                Error.NotFound("Service not found."));
        }

        // ============================================================
        // 2. Validate Service Category
        // ============================================================

        var categoryExistsResult =
            await _repository.CategoryExistsAsync(
                request.ServiceCategoryId,
                cancellationToken);

        if (categoryExistsResult.IsFailure)
        {
            return Result<UpdateServiceResponse>
                .Failure(categoryExistsResult.Error);
        }

        if (!categoryExistsResult.Value)
        {
            return Result<UpdateServiceResponse>.Failure(
                Error.NotFound("Service Category not found."));
        }

        // ============================================================
        // 3. Update Domain Entity
        // ============================================================

        service.Update(
            request.ServiceCategoryId,
            request.ServiceName,
            request.Description,
            request.SpecialInstruction,
            request.HasBranchPermission,
            request.AllowBranchEditPrice,
            _userContext.StaffId);

        // ============================================================
        // 4. Mark Entity as Modified
        // ============================================================

        _repository.Update(service);

        // ============================================================
        // 5. Commit Changes
        // ============================================================

        var saveResult =
            await _unitOfWork.SaveChangesAsync(cancellationToken);

        if (saveResult.IsFailure)
        {
            return Result<UpdateServiceResponse>
                .Failure(saveResult.Error);
        }

        // ============================================================
        // 6. Return Response
        // ============================================================

        var response = new UpdateServiceResponse(
            service.ServiceId,
            service.ServiceName!);

        return Result<UpdateServiceResponse>
            .Success(response);
    }
}