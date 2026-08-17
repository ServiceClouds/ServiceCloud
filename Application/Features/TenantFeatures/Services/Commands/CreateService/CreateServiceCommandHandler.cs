using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.ServiceEntities;
using Shared.Response;

namespace Application.Features.TenantFeatures.Services.Commands.CreateService;

public sealed class CreateServiceCommandHandler
    : ICommandHandler<CreateServiceCommand, CreateServiceResponse>
{
    private readonly IServiceRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public CreateServiceCommandHandler(
        IServiceRepository repository,
        IUnitOfWork unitOfWork,
        IUserContext userContext)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<Result<CreateServiceResponse>> Handle(
        CreateServiceCommand request,
        CancellationToken cancellationToken)
    {
        // ============================================================
        // 1. Validate Service Category
        // ============================================================

        var categoryExistsResult =
            await _repository.CategoryExistsAsync(
                request.ServiceCategoryId,
                cancellationToken);

        if (categoryExistsResult.IsFailure)
        {
            return Result<CreateServiceResponse>
                .Failure(categoryExistsResult.Error);
        }

        if (!categoryExistsResult.Value)
        {
            return Result<CreateServiceResponse>.Failure(
                Error.NotFound("Service Category not found."));
        }

        // ============================================================
        // 2. Create Domain Entity
        // ============================================================

        var service = Service.Create(
            request.ServiceCategoryId,
            request.ServiceName,
            request.Description,
            _userContext.StaffId,
            _userContext.CompanyId,
            request.SpecialInstruction,
            request.HasBranchPermission,
            request.AllowBranchEditPrice,
            request.AppSourceTypeId);

        // ============================================================
        // 3. Add Entity
        // ============================================================
        // Generic repository handles adding the entity.
        // No database context is accessed directly here.

        _repository.Add(service);

        // ============================================================
        // 4. Commit Transaction
        // ============================================================

        var saveResult =
            await _unitOfWork.SaveChangesAsync(cancellationToken);

        if (saveResult.IsFailure)
        {
            return Result<CreateServiceResponse>
                .Failure(saveResult.Error);
        }

        // ============================================================
        // 5. Return Response
        // ============================================================

        var response = new CreateServiceResponse(
            service.ServiceId,
            service.ServiceName!);

        return Result<CreateServiceResponse>
            .Success(response);
    }
}