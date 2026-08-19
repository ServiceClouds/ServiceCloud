using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Products;
using Shared.Response;

namespace Application.Features.TenantFeatures.ProductCategories.Commands.UpdateProductCategory;




public sealed class UpdateProductCategoryCommandHandler
    : ICommandHandler<UpdateProductCategoryCommand, UpdateProductCategoryResponse>
{
    private readonly ITenantRepository<ProductCategory> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public UpdateProductCategoryCommandHandler(
        ITenantRepository<ProductCategory> repository,
        IUnitOfWork unitOfWork,
        IUserContext userContext)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<Result<UpdateProductCategoryResponse>> Handle(
        UpdateProductCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var category = await _repository.FirstOrDefaultAsync(
            x => x.ProductCategoryId == request.ProductCategoryId &&
                 !x.IsArchived,
            false,
            cancellationToken);

        if (category is null)
        {
            return Result<UpdateProductCategoryResponse>.Failure(
                Error.NotFound("Product Category not found."));
        }

        category.Update(
            _userContext.StaffId,
            request.ProductCategoryName,
            request.Description,
            request.ImagePath,
            request.HasBranchPermission,
            _userContext.CompanyId,
            request.AppSourceTypeId);

        _repository.Update(category);

        var saveResult =
            await _unitOfWork.SaveChangesAsync(cancellationToken);

        if (saveResult.IsFailure)
            return Result<UpdateProductCategoryResponse>
                .Failure(saveResult.Error);

        return Result<UpdateProductCategoryResponse>.Success(
            new UpdateProductCategoryResponse(
                category.ProductCategoryId,
                category.ProductCategoryName));
    }
}