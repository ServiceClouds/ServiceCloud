using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Products;
using Shared.Response;

namespace Application.Features.TenantFeatures.ProductVariantPackagings.Commands.CreateProductVariantPackaging;

public sealed class CreateProductVariantPackagingCommandHandler
    : ICommandHandler<
        CreateProductVariantPackagingCommand,
        CreateProductVariantPackagingResponse>
{
    private readonly ITenantRepository<ProductVariantPackaging> _repository;
    private readonly ITenantRepository<ProductVariant> _productVariantRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductVariantPackagingCommandHandler(
        ITenantRepository<ProductVariantPackaging> repository,
        ITenantRepository<ProductVariant> productVariantRepository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _productVariantRepository = productVariantRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CreateProductVariantPackagingResponse>> Handle(
        CreateProductVariantPackagingCommand request,
        CancellationToken cancellationToken)
    {
        var productVariant =
            await _productVariantRepository.FirstOrDefaultAsync(
                x =>
                    x.ProductVariantId == request.ProductVariantId &&
                    !x.IsArchived,
                cancellationToken: cancellationToken);

        if (productVariant is null)
        {
            return Result<CreateProductVariantPackagingResponse>.Failure(
                Error.NotFound("Product variant not found."));
        }

        var packaging = ProductVariantPackaging.Create(
            request.ProductVariantId,
            request.ProductVariantId,
            request.Weight,
            request.WeightUnitId,
            request.DimensionUnitId,
            request.Length,
            request.Width,
            request.Height,
            request.SizeVolume,
            request.SizeVolumeUnitId);

        _repository.Add(packaging);

        var saveResult =
            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

        if (saveResult.IsFailure)
        {
            return Result<CreateProductVariantPackagingResponse>
                .Failure(saveResult.Error);
        }

        return Result<CreateProductVariantPackagingResponse>.Success(
            new CreateProductVariantPackagingResponse(
                packaging.ProductVariantPackagingId,
                packaging.ProductVariantId));
    }
}