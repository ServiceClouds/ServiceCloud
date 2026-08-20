using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Entities;
using Shared.Response;

namespace Application.Features.TenantFeatures.Currencies.Commands.UpdateCurrency;

public sealed class UpdateCurrencyCommandHandler
    : ICommandHandler<UpdateCurrencyCommand>
{
    private readonly ITenantRepository<Currency> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public UpdateCurrencyCommandHandler(
        ITenantRepository<Currency> repository,
        IUnitOfWork unitOfWork,
        IUserContext userContext)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<Result> Handle(
        UpdateCurrencyCommand request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.CurrencyName))
        {
            return Result.Failure(
                Error.Conflict("Currency name is required."));
        }

        if (string.IsNullOrWhiteSpace(request.CurrencyCode))
        {
            return Result.Failure(
                Error.Conflict("Currency code is required."));
        }

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

        currency.Update(
            request.CurrencyName.Trim(),
            request.CurrencyCode.Trim(),
            _userContext.StaffId);

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