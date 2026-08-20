using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Entities;
using Shared.Response;

namespace Application.Features.TenantFeatures.Currencies.Commands.ArchiveCurrency;

public sealed class ArchiveCurrencyCommandHandler
    : ICommandHandler<ArchiveCurrencyCommand>
{
    private readonly ITenantRepository<Currency> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public ArchiveCurrencyCommandHandler(
        ITenantRepository<Currency> repository,
        IUnitOfWork unitOfWork,
        IUserContext userContext)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<Result> Handle(
        ArchiveCurrencyCommand request,
        CancellationToken cancellationToken)
    {
        var currency =
            await _repository.FirstOrDefaultAsync(
                x =>
                    x.CurrencyId == request.CurrencyId &&
                    x.IsActive,
                asNoTracking: false,
                cancellationToken);

        if (currency is null)
        {
            return Result.Failure(
                Error.NotFound("Currency not found."));
        }

        currency.Archive(_userContext.StaffId);

        _repository.Update(currency);

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