using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Products;
using Shared.Response;

namespace Application.Features.TenantFeatures.ProductAttributeValues.Commands.CreateProductAttributeValue;

public sealed class CreateProductAttributeValueCommandHandler
    : ICommandHandler<CreateProductAttributeValueCommand, CreateProductAttributeValueResponse>
{
    private readonly ITenantRepository<ProductAttributeValue> _repository;
    private readonly ITenantRepository<ProductAttribute> _productAttributeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductAttributeValueCommandHandler(
        ITenantRepository<ProductAttributeValue> repository,
        ITenantRepository<ProductAttribute> productAttributeRepository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _productAttributeRepository = productAttributeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CreateProductAttributeValueResponse>> Handle(
        CreateProductAttributeValueCommand request,
        CancellationToken cancellationToken)
    {
        var productAttributeExists =
            await _productAttributeRepository.ExistsAsync(
                x => x.ProductAttributeId == request.ProductAttributeId,
                cancellationToken);

        if (!productAttributeExists)
        {
            return Result<CreateProductAttributeValueResponse>.Failure(
                Error.NotFound("Product attribute was not found."));
        }

        var exists =
            await _repository.ExistsAsync(
                x =>
                    x.ProductAttributeId == request.ProductAttributeId &&
                    x.AttributeValueId == request.AttributeValueId,
                cancellationToken);

        if (exists)
        {
            return Result<CreateProductAttributeValueResponse>.Failure(
                Error.Conflict("This attribute value already exists."));
        }

        var productAttributeValue =
            ProductAttributeValue.Create(
                request.ProductAttributeId,
                request.AttributeValueId);

        _repository.Add(productAttributeValue);

        var saveResult =
            await _unitOfWork.SaveChangesAsync(cancellationToken);

        if (saveResult.IsFailure)
        {
            return Result<CreateProductAttributeValueResponse>
                .Failure(saveResult.Error);
        }

        return Result<CreateProductAttributeValueResponse>.Success(
            new CreateProductAttributeValueResponse(
                productAttributeValue.ProductAttributeValueId,
                productAttributeValue.ProductAttributeId,
                productAttributeValue.AttributeValueId));
    }
}
