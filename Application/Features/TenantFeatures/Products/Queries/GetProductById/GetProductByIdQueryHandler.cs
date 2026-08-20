using Application.Abstractions.Queries;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Products;
using Shared.Response;

namespace Application.Features.TenantFeatures.Products.Queries.GetProductById;

public sealed class GetProductByIdQueryHandler
    : IQueryHandler<GetProductByIdQuery, ProductResponse>
{
    private readonly ITenantRepository<Product> _repository;

    public GetProductByIdQueryHandler(
        ITenantRepository<Product> repository)
    {
        _repository = repository;
    }

    public async Task<Result<ProductResponse>> Handle(
        GetProductByIdQuery request,
        CancellationToken cancellationToken)
    {
        var product =
            await _repository.FirstOrDefaultAsync(
                x =>
                    x.ProductId == request.ProductId &&
                    x.IsArchived!=true,
               cancellationToken: cancellationToken);

        if (product is null)
        {
            return Result<ProductResponse>.Failure(
                Error.NotFound("Product was not found."));
        }

        return Result<ProductResponse>.Success(
            new ProductResponse(
                product.ProductId,
                product.ProductCategoryId,
                product.ProductName,
                product.Description,
                product.IsActive,
                product.AllowBranchTrackInventory,
                product.HasBranchPermission,
                product.AllowBranchEditPrice,
                product.ProductClassificationId,
                product.BrandId,
                product.AppSourceTypeId));
    }
}
