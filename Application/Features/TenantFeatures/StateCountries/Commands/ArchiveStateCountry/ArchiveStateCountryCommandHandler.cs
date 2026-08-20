using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Tenant.ServiceCloudTenant.Entities;
using Shared.Response;

namespace Application.Features.TenantFeatures.StateCountries.Commands.ArchiveStateCountry;

public sealed class ArchiveStateCountryCommandHandler
    : ICommandHandler<ArchiveStateCountryCommand>
{
    private readonly ITenantRepository<StateCountry> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public ArchiveStateCountryCommandHandler(
        ITenantRepository<StateCountry> repository,
        IUnitOfWork unitOfWork,
        IUserContext userContext)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<Result> Handle(
        ArchiveStateCountryCommand request,
        CancellationToken cancellationToken)
    {
        var stateCountry =
            await _repository.FirstOrDefaultAsync(
                x =>
                    x.StateCountryId == request.StateCountryId &&
                    x.IsActive,
                asNoTracking: false,
                cancellationToken);

        if (stateCountry is null)
        {
            return Result.Failure(
                Error.NotFound("State/Country not found."));
        }

        stateCountry.Archive(
            _userContext.StaffId);

        _repository.Update(stateCountry);

        var saveResult =
            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

        if (saveResult.IsFailure)
        {
            return Result.Failure(saveResult.Error);
        }

        return Result.Success();
    }
}