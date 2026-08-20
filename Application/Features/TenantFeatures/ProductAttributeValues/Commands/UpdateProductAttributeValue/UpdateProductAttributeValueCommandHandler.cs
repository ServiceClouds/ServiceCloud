using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Products;
using Shared.Response;

namespace Application.Features.TenantFeatures.ProductAttributeValues.Commands.UpdateProductAttributeValue;

public sealed class UpdateProductAttributeValueCommandHandler
    : ICommandHandler<UpdateProductAttributeValueCommand, UpdateProductAttributeValueResponse>
{
    private readonly ITenantRepository<ProductAttributeValue> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductAttributeValueCommandHandler(
        ITenantRepository<ProductAttributeValue> repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<UpdateProductAttributeValueResponse>> Handle(
        UpdateProductAttributeValueCommand request,
        CancellationToken cancellationToken)
    {
        var productAttributeValue =
            await _repository.FirstOrDefaultAsync(
                x => x.ProductAttributeValueId == request.ProductAttributeValueId,
                cancellationToken: cancellationToken);

        if (productAttributeValue is null)
        {
            return Result<UpdateProductAttributeValueResponse>.Failure(
                Error.NotFound("Product attribute value was not found."));
        }

        var duplicate =
            await _repository.ExistsAsync(
                x =>
                    x.ProductAttributeValueId != request.ProductAttributeValueId &&
                    x.ProductAttributeId == productAttributeValue.ProductAttributeId &&
                    x.AttributeValueId == request.AttributeValueId,
                cancellationToken);

        if (duplicate)
        {
            return Result<UpdateProductAttributeValueResponse>.Failure(
                Error.Conflict("This attribute value already exists."));
        }

        productAttributeValue.Update(request.AttributeValueId);

        _repository.Update(productAttributeValue);

        var saveResult =
            await _unitOfWork.SaveChangesAsync(cancellationToken);

        if (saveResult.IsFailure)
        {
            return Result<UpdateProductAttributeValueResponse>
                .Failure(saveResult.Error);
        }

        return Result<UpdateProductAttributeValueResponse>.Success(
            new UpdateProductAttributeValueResponse(
                productAttributeValue.ProductAttributeValueId,
                productAttributeValue.ProductAttributeId,
                productAttributeValue.AttributeValueId));
    }
}
