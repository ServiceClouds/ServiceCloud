using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories;
using Domain.Entities.Tenant.ServiceCloudTenant.ServiceEntities;
using Shared.Response;

namespace Application.Features.Services.Commands.CreateService;

public sealed class CreateServiceCommandHandler
    : ICommandHandler<CreateServiceCommand, CreateServiceResponse>
{
    private readonly IServiceRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateServiceCommandHandler(
        IServiceRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CreateServiceResponse>> Handle(
        CreateServiceCommand request,
        CancellationToken cancellationToken)
    {
        // 1. Check Category

        var categoryExists = await _repository.CategoryExistsAsync(
            request.CompanyId,
            request.ServiceCategoryId,
            cancellationToken);

        if (categoryExists.IsFailure)
            return Result<CreateServiceResponse>.Failure(categoryExists.Error);

        if (!categoryExists.Value)
            return Result<CreateServiceResponse>.Failure(
                Error.NotFound("Service Category not found."));

        // 2. Create Entity

        var service = Service.Create(
            request.ServiceCategoryId,
            request.ServiceName,
            request.Description,
            request.CreatedBy,
            request.CompanyId,
            request.SpecialInstruction,
            request.HasBranchPermission,
            request.AllowBranchEditPrice,
            request.AppSourceTypeId);

        // 3. Add

        var addResult = await _repository.AddAsync(
            request.CompanyId,
            service,
            cancellationToken);

        if (addResult.IsFailure)
            return Result<CreateServiceResponse>.Failure(addResult.Error);

        // 4. Save

        var saveResult = await _unitOfWork.SaveChangesAsync(
            request.CompanyId,
            cancellationToken);

        if (saveResult.IsFailure)
            return Result<CreateServiceResponse>.Failure(saveResult.Error);

        // 5. Return response
        var response = new CreateServiceResponse(
        service.ServiceId,
        service.ServiceName!
);
        return Result<CreateServiceResponse>.Success(response);
    }
}