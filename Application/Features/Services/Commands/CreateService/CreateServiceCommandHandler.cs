using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.ServiceEntities;
using Shared.Response;

namespace Application.Features.Services.Commands.CreateService;

public sealed class CreateServiceCommandHandler
    : ICommandHandler<CreateServiceCommand, CreateServiceResponse>
{
    private readonly IServiceRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public CreateServiceCommandHandler(
        IServiceRepository repository,
        IUnitOfWork unitOfWork,
        IUserContext userContext
        )
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<Result<CreateServiceResponse>> Handle(
        CreateServiceCommand request,
        CancellationToken cancellationToken)
    {
        // 1. Check Category

        var categoryExists = await _repository.CategoryExistsAsync(
            
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
            _userContext.StaffId,
            _userContext.CompanyId,
            request.SpecialInstruction,
            request.HasBranchPermission,
            request.AllowBranchEditPrice,
            request.AppSourceTypeId);

        // 3. Add

        var addResult = await _repository.AddAsync(
      
            service,
            cancellationToken);

        if (addResult.IsFailure)
            return Result<CreateServiceResponse>.Failure(addResult.Error);

        // 4. Save

        var saveResult = await _unitOfWork.SaveChangesAsync(
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