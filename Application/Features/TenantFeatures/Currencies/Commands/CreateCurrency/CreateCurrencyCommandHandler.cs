using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Entities;
using Shared.Response;

namespace Application.Features.TenantFeatures.Currencies.Commands.CreateCurrency;

public sealed class CreateCurrencyCommandHandler
    : ICommandHandler<CreateCurrencyCommand, CreateCurrencyResponse>
{
    private readonly ITenantRepository<Currency> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public CreateCurrencyCommandHandler(
        ITenantRepository<Currency> repository,
        IUnitOfWork unitOfWork,
        IUserContext userContext)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<Result<CreateCurrencyResponse>> Handle(
        CreateCurrencyCommand request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.CurrencyName))
        {
            return Result<CreateCurrencyResponse>.Failure(
                Error.Conflict("Currency name is required."));
        }

        if (string.IsNullOrWhiteSpace(request.CurrencyCode))
        {
            return Result<CreateCurrencyResponse>.Failure(
                Error.Conflict("Currency code is required."));
        }

        var currency = Currency.Create(
            request.CurrencyName.Trim(),
            request.CurrencyCode.Trim(),
            _userContext.StaffId);

        _repository.Add(currency);

        var saveResult =
            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

        if (saveResult.IsFailure)
        {
            return Result<CreateCurrencyResponse>
                .Failure(saveResult.Error);
        }

        return Result<CreateCurrencyResponse>.Success(
            new CreateCurrencyResponse(
                currency.CurrencyId,
                currency.CurrencyName,
                currency.CurrencyCode));
    }
}