using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories;
using Shared.Response;

namespace Application.Features.Services.Commands.UpdateService;

public sealed class UpdateServiceCommandHandler
    : ICommandHandler<UpdateServiceCommand, UpdateServiceResponse>
{
    private readonly IServiceRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateServiceCommandHandler(
        IServiceRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<UpdateServiceResponse>> Handle(
        UpdateServiceCommand request,
        CancellationToken cancellationToken)
    {
        // 1. Check service exists
        var serviceResult = await _repository.GetByIdAsync(
            request.CompanyId,
            request.ServiceId,
            cancellationToken);

        if (serviceResult.IsFailure)
            return Result<UpdateServiceResponse>.Failure(serviceResult.Error);

        // 2. Check category exists
        var categoryResult = await _repository.CategoryExistsAsync(
            request.CompanyId,
            request.ServiceCategoryId,
            cancellationToken);

        if (categoryResult.IsFailure)
            return Result<UpdateServiceResponse>.Failure(categoryResult.Error);

        if (!categoryResult.Value)
            return Result<UpdateServiceResponse>.Failure(
                Error.NotFound("Service Category not found."));

        var service = serviceResult.Value!;

        // 3. Update entity
        service.Update(
            request.ServiceCategoryId,
            request.ServiceName,
            request.Description,
            request.SpecialInstruction,
            request.HasBranchPermission,
            request.AllowBranchEditPrice,
            request.ModifiedBy);

        // 4. Mark entity as modified
        var updateResult = await _repository.UpdateAsync(
            request.CompanyId,
            service);

        if (updateResult.IsFailure)
            return Result<UpdateServiceResponse>.Failure(updateResult.Error);

        // 5. Save changes
        var saveResult = await _unitOfWork.SaveChangesAsync(
            request.CompanyId,
            cancellationToken);

        if (saveResult.IsFailure)
            return Result<UpdateServiceResponse>.Failure(saveResult.Error);

        // 6. Return response
        var response = new UpdateServiceResponse(
            service.ServiceId,
            service.ServiceName!);

        return Result<UpdateServiceResponse>.Success(response);
    }
}