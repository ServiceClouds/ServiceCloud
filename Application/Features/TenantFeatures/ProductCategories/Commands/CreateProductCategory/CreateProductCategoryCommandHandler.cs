using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Products;
using Shared.Response;

namespace Application.Features.TenantFeatures.ProductCategories.Commands.CreateProductCategory;



public sealed class CreateProductCategoryCommandHandler
    : ICommandHandler<CreateProductCategoryCommand, CreateProductCategoryResponse>
{
    private readonly ITenantRepository<ProductCategory> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public CreateProductCategoryCommandHandler(
        ITenantRepository<ProductCategory> repository,
        IUnitOfWork unitOfWork,
        IUserContext userContext)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<Result<CreateProductCategoryResponse>> Handle(
        CreateProductCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var category = ProductCategory.Create(
            _userContext.StaffId,
            request.ProductCategoryName,
            request.Description,
            request.ImagePath,
            request.HasBranchPermission,
            false,
            _userContext.CompanyId,
            request.AppSourceTypeId);

        _repository.Add(category);

        var saveResult =
            await _unitOfWork.SaveChangesAsync(cancellationToken);

        if (saveResult.IsFailure)
            return Result<CreateProductCategoryResponse>
                .Failure(saveResult.Error);

        return Result<CreateProductCategoryResponse>.Success(
            new CreateProductCategoryResponse(
                category.ProductCategoryId,
                category.ProductCategoryName));
    } }
