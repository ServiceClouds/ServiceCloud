using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.ServiceEntities;
using Shared.Response;


namespace Application.Features.TenantFeatures.ServiceCategories.Commands.ArchiveServiceCategory
{


    public sealed class ArchiveServiceCategoryCommandHandler
        : ICommandHandler<ArchiveServiceCategoryCommand>
    {
        private readonly ITenantRepository<ServiceCategory> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;

        public ArchiveServiceCategoryCommandHandler(
            ITenantRepository<ServiceCategory> repository,
            IUnitOfWork unitOfWork,
            IUserContext userContext)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _userContext = userContext;
        }

        public async Task<Result> Handle(
            ArchiveServiceCategoryCommand request,
            CancellationToken cancellationToken)
        {
            var category = await _repository.FirstOrDefaultAsync(
                x => x.ServiceCategoryId == request.ServiceCategoryId &&
                     x.CompanyId == _userContext.CompanyId,
                asNoTracking: false,
                cancellationToken: cancellationToken);

            if (category is null)
            {
                return Result.Failure(
                    Error.NotFound("Service category not found."));
            }

            category.Archive(_userContext.StaffId);

            _repository.Update(category);

            var saveResult =
                await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (saveResult.IsFailure)
            {
                return Result.Failure(saveResult.Error);
            }

            return Result.Success();
        }
    }
}
