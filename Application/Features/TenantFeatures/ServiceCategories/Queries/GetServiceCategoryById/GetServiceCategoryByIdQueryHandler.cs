
using Application.Abstractions.Queries;
using Application.Abstractions.Repositories.Common;
using Shared.Response;
using Domain.Entities.Tenant.ServiceCloudTenant.ServiceEntities;
using Application.Common;

namespace Application.Features.TenantFeatures.ServiceCategories.Queries.GetServiceCategoryById
{
    

    public sealed class GetServiceCategoryByIdQueryHandler
        : IQueryHandler<GetServiceCategoryByIdQuery, GetServiceCategoryByIdResponse>
    {
        private readonly IGenericRepository<ServiceCategory> _repository;
        private readonly IUserContext _userContext;

        public GetServiceCategoryByIdQueryHandler(
            IGenericRepository<ServiceCategory> repository,
            IUserContext userContext)
        {
            _repository = repository;
            _userContext = userContext;
        }

        public async Task<Result<GetServiceCategoryByIdResponse>> Handle(
            GetServiceCategoryByIdQuery request,
            CancellationToken cancellationToken)
        {
            var category = await _repository.FirstOrDefaultAsync(
                x => x.ServiceCategoryId == request.ServiceCategoryId &&
                     x.CompanyId == _userContext.CompanyId,
                cancellationToken: cancellationToken);

            if (category is null)
            {
                return Result<GetServiceCategoryByIdResponse>.Failure(
                    Error.NotFound("Service category not found."));
            }

            return Result<GetServiceCategoryByIdResponse>.Success(
                new GetServiceCategoryByIdResponse(
                    category.ServiceCategoryId,
                    category.ServiceCategoryName,
                    category.Description,
                    category.ImagePath,
                    category.HasBranchPermission,
                    category.AppSourceTypeId,
                    category.Color,
                    category.SortIndex,
                    category.IsArchived,
                    category.CompanyId));
        }
    }
}
