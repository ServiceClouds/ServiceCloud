using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Products;
using Shared.Response;
namespace Application.Features.TenantFeatures.ProductCategories.Commands.ArchiveProductCategory {



    public sealed class ArchiveProductCategoryCommandHandler
        : ICommandHandler<ArchiveProductCategoryCommand>
    {
        private readonly ITenantRepository<ProductCategory> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;

        public ArchiveProductCategoryCommandHandler(
            ITenantRepository<ProductCategory> repository,
            IUnitOfWork unitOfWork,
            IUserContext userContext)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _userContext = userContext;
        }

        public async Task<Result> Handle(
            ArchiveProductCategoryCommand request,
            CancellationToken cancellationToken)
        {
            var category = await _repository.FirstOrDefaultAsync(
                x => x.ProductCategoryId == request.ProductCategoryId &&
                     !x.IsArchived,
                false,
                cancellationToken);

            if (category is null)
            {
                return Result.Failure(
                    Error.NotFound("Product Category not found."));
            }

            category.Archive(_userContext.StaffId);

            _repository.Update(category);

            return await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}