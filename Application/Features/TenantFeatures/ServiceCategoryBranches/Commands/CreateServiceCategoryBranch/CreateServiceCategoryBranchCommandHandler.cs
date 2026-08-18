using Application.Abstractions.Commands;
using Application.Abstractions.Repositories.Common;
using Application.Abstractions.Data;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.ServiceEntities;
using Shared.Response;
using Application.Features.TenantFeatures.ServiceCategoryBranches;


namespace Application.Features.TenantFeatures.ServiceCategoryBranches.Commands.CreateServiceCategoryBranch
{


    public sealed class CreateServiceCategoryBranchCommandHandler
        : ICommandHandler<
            CreateServiceCategoryBranchCommand,
            CreateServiceCategoryBranchResponse>
    {
        private readonly IGenericRepository<ServiceCategoryBranch> _repository;
        private readonly IGenericRepository<ServiceCategory> _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;

        public CreateServiceCategoryBranchCommandHandler(
            IGenericRepository<ServiceCategoryBranch> repository,
            IGenericRepository<ServiceCategory> categoryRepository,
            IUnitOfWork unitOfWork,
            IUserContext userContext)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
            _userContext = userContext;
        }

        public async Task<Result<CreateServiceCategoryBranchResponse>> Handle(
            CreateServiceCategoryBranchCommand request,
            CancellationToken cancellationToken)
        {
            // 1. Validate Service Category
            var category = await _categoryRepository.FirstOrDefaultAsync(
                x => x.ServiceCategoryId == request.ServiceCategoryId &&
                     x.CompanyId == _userContext.CompanyId &&
                     !x.IsArchived,
                cancellationToken: cancellationToken);

            if (category is null)
            {
                return Result<CreateServiceCategoryBranchResponse>.Failure(
                    Error.NotFound(
                        "ServiceCategory.NotFound Service category not found."));
            }

            // 2. Check duplicate mapping
            var alreadyExists = await _repository.ExistsAsync(
                x => x.ServiceCategoryId == request.ServiceCategoryId &&
                     x.BranchId == request.BranchId,
                cancellationToken);

            if (alreadyExists)
            {
                return Result<CreateServiceCategoryBranchResponse>.Failure(
                    Error.Conflict(
                        "ServiceCategoryBranch.AlreadyExists " +
                        "This service category is already mapped to this branch."));
            }

            // 3. Create entity
            var serviceCategoryBranch = ServiceCategoryBranch.Create(
                request.ServiceCategoryId,
                request.BranchId,
                request.IsIncluded);

            // 4. Add
            _repository.Add(serviceCategoryBranch);

            // 5. Save
            var saveResult =
                await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (saveResult.IsFailure)
            {
                return Result<CreateServiceCategoryBranchResponse>
                    .Failure(saveResult.Error);
            }

            // 6. Response
            return Result<CreateServiceCategoryBranchResponse>.Success(
                new CreateServiceCategoryBranchResponse(
                    serviceCategoryBranch.ServiceCategoryBranchId,
                    serviceCategoryBranch.ServiceCategoryId,
                    serviceCategoryBranch.BranchId,
                    serviceCategoryBranch.IsActive,
                    serviceCategoryBranch.IsIncluded));
        }
    }
}
