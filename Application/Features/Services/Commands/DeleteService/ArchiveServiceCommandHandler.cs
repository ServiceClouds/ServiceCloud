using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories;
using Shared.Response;

namespace Application.Features.Services.Commands.ArchiveService;

public sealed class ArchiveServiceCommandHandler
    : ICommandHandler<ArchiveServiceCommand, ArchiveServiceResponse>
{
    private readonly IServiceRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public ArchiveServiceCommandHandler(
        IServiceRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<ArchiveServiceResponse>> Handle(
        ArchiveServiceCommand request,
        CancellationToken cancellationToken)
    {
        // 1. Get existing service
        var serviceResult = await _repository.GetByIdAsync(
            request.CompanyId,
            request.ServiceId,
            cancellationToken);

        if (serviceResult.IsFailure)
            return Result<ArchiveServiceResponse>.Failure(serviceResult.Error);

        var service = serviceResult.Value!;

        // 2. Archive entity
        service.Archive(request.ModifiedBy);

        // 3. Mark entity as modified
        var updateResult = await _repository.UpdateAsync(
            request.CompanyId,
            service);

        if (updateResult.IsFailure)
            return Result<ArchiveServiceResponse>.Failure(updateResult.Error);

        // 4. Save changes
        var saveResult = await _unitOfWork.SaveChangesAsync(
            request.CompanyId,
            cancellationToken);

        if (saveResult.IsFailure)
            return Result<ArchiveServiceResponse>.Failure(saveResult.Error);

        // 5. Response
        var response = new ArchiveServiceResponse(
            service.ServiceId,
            service.ServiceName!
        );

        return Result<ArchiveServiceResponse>.Success(response);
    }
}