using Application.Abstractions.Queries;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Products;
using Microsoft.EntityFrameworkCore;
using Shared.Response;

namespace Application.Features.TenantFeatures.Products.Queries.GetAllProducts;

public sealed class GetAllProductsQueryHandler
    : IQueryHandler<GetAllProductsQuery, List<ProductResponse>>
{
    private readonly ITenantRepository<Product> _repository;

    public GetAllProductsQueryHandler(
        ITenantRepository<Product> repository)
    {
        _repository = repository;
    }

    public async Task<Result<List<ProductResponse>>> Handle(
        GetAllProductsQuery request,
        CancellationToken cancellationToken)
    {
        var products =
     await _repository
         .GetAll()
         .Where(x => x.IsArchived != true)
         .OrderBy(x => x.ProductName)
         .Select(x => new ProductResponse(
             x.ProductId,
             x.ProductCategoryId,
             x.ProductName,
             x.Description,
             x.IsActive,
             x.AllowBranchTrackInventory,
             x.HasBranchPermission,
             x.AllowBranchEditPrice,
             x.ProductClassificationId,
             x.BrandId,
             x.AppSourceTypeId))
         .ToListAsync(cancellationToken);

        return Result<List<ProductResponse>>.Success(products);
    }
}
