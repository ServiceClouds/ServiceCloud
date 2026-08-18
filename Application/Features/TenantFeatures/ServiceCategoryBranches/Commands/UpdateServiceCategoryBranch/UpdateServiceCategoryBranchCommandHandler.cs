using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.ServiceEntities;
using Shared.Response;


namespace Application.Features.TenantFeatures.ServiceCategoryBranches.Commands.UpdateServiceCategoryBranch
{
    public sealed class UpdateServiceCategoryBranchCommandHandler
        : ICommandHandler<
            UpdateServiceCategoryBranchCommand,
            UpdateServiceCategoryBranchResponse>
    {
        private readonly ITenantRepository<ServiceCategoryBranch> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;

        public UpdateServiceCategoryBranchCommandHandler(
            ITenantRepository<ServiceCategoryBranch> repository,
            IUnitOfWork unitOfWork,
            IUserContext userContext)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _userContext = userContext;
        }

        public async Task<Result<UpdateServiceCategoryBranchResponse>> Handle(
            UpdateServiceCategoryBranchCommand request,
            CancellationToken cancellationToken)
        {
            var serviceCategoryBranch = await _repository.FirstOrDefaultAsync(
                x => x.ServiceCategoryBranchId == request.ServiceCategoryBranchId,
                asNoTracking: false,
                cancellationToken: cancellationToken);

            if (serviceCategoryBranch is null)
            {
                return Result<UpdateServiceCategoryBranchResponse>.Failure(
                    Error.NotFound(
                        "ServiceCategoryBranch.NotFound Service category branch not found."));
            }

            serviceCategoryBranch.UpdateStatus(
                request.IsActive,
                request.IsIncluded);

            var saveResult =
                await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (saveResult.IsFailure)
            {
                return Result<UpdateServiceCategoryBranchResponse>
                    .Failure(saveResult.Error);
            }

            return Result<UpdateServiceCategoryBranchResponse>.Success(
                new UpdateServiceCategoryBranchResponse(
                    serviceCategoryBranch.ServiceCategoryBranchId,
                    serviceCategoryBranch.ServiceCategoryId,
                    serviceCategoryBranch.BranchId,
                    serviceCategoryBranch.IsActive,
                    serviceCategoryBranch.IsIncluded));
        }
    }
}