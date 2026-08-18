using Application.Abstractions.Queries;
using Application.Abstractions.Repositories;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.ServiceEntities;
using Shared.Response;

namespace Application.Features.TenantFeatures.ServiceCategories.Queries.GetPagedServiceCategories
{





    public sealed class GetPagedServiceCategoriesQueryHandler
        : IQueryHandler<
            GetPagedServiceCategoriesQuery,
            PagedResponse<ServiceCategory>>
    {
        private readonly IServiceCategoryRepository _repository;

        public GetPagedServiceCategoriesQueryHandler(
            IServiceCategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<PagedResponse<ServiceCategory>>> Handle(
            GetPagedServiceCategoriesQuery request,
            CancellationToken cancellationToken)
        {
            return await _repository.GetPagedAsync(
                request.Request,
                cancellationToken);
        }
    }
}
