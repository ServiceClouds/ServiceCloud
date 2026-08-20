using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Products;
using Shared.Response;

namespace Application.Features.TenantFeatures.ProductVariantPackagings.Commands.UpdateProductVariantPackaging;

public sealed class UpdateProductVariantPackagingCommandHandler
    : ICommandHandler<UpdateProductVariantPackagingCommand>
{
    private readonly ITenantRepository<ProductVariantPackaging> _repository;
    private readonly ITenantRepository<ProductVariant> _productVariantRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductVariantPackagingCommandHandler(
        ITenantRepository<ProductVariantPackaging> repository,
        ITenantRepository<ProductVariant> productVariantRepository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _productVariantRepository = productVariantRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        UpdateProductVariantPackagingCommand request,
        CancellationToken cancellationToken)
    {
        var packaging =
            await _repository.FirstOrDefaultAsync(
                x =>
                    x.ProductVariantPackagingId ==
                    request.ProductVariantPackagingId,
                asNoTracking: false,
                cancellationToken);

        if (packaging is null)
        {
            return Result.Failure(
                Error.NotFound(
                    "Product variant packaging not found."));
        }

        var productVariant =
            await _productVariantRepository.FirstOrDefaultAsync(
                x =>
                    x.ProductVariantId == packaging.ProductVariantId &&
                    !x.IsArchived,
                cancellationToken: cancellationToken);

        if (productVariant is null)
        {
            return Result.Failure(
                Error.NotFound("Product variant not found."));
        }

        packaging.Update(
            request.Weight,
            request.WeightUnitId,
            request.DimensionUnitId,
            request.Length,
            request.Width,
            request.Height,
            request.SizeVolume,
            request.SizeVolumeUnitId);

        _repository.Update(packaging);

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