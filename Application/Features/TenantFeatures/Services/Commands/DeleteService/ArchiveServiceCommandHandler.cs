using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories;
using Application.Common;
using Shared.Response;

namespace Application.Features.TenantFeatures.Services.Commands.DeleteService;

public sealed class ArchiveServiceCommandHandler
    : ICommandHandler<ArchiveServiceCommand, ArchiveServiceResponse>
{
    private readonly IServiceRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public ArchiveServiceCommandHandler(
        IServiceRepository repository,
        IUnitOfWork unitOfWork,
        IUserContext userContext)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<Result<ArchiveServiceResponse>> Handle(
        ArchiveServiceCommand request,
        CancellationToken cancellationToken)
    {
        // ============================================================
        // 1. Get the existing active service
        // ============================================================

        var service = await _repository.FirstOrDefaultAsync(
            x =>
                x.ServiceId == request.ServiceId &&
                !x.IsArchived,
            cancellationToken: cancellationToken);

        if (service is null)
        {
            return Result<ArchiveServiceResponse>.Failure(
                Error.NotFound("Service not found."));
        }

        // ============================================================
        // 2. Archive the entity through the domain method
        // ============================================================

        service.Archive(_userContext.StaffId);

        // ============================================================
        // 3. Mark the entity as modified
        // ============================================================

        _repository.Update(service);

        // ============================================================
        // 4. Save changes
        // ============================================================

        var saveResult =
            await _unitOfWork.SaveChangesAsync(cancellationToken);

        if (saveResult.IsFailure)
        {
            return Result<ArchiveServiceResponse>.Failure(
                saveResult.Error);
        }

        // ============================================================
        // 5. Return response
        // ============================================================

        var response = new ArchiveServiceResponse(
            service.ServiceId,
            service.ServiceName!);

        return Result<ArchiveServiceResponse>.Success(response);
    }
}