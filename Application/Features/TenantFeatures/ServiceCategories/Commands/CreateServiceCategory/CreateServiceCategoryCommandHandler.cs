using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.ServiceEntities;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ServiceCategories.Commands.CreateServiceCategory
{


    public sealed class CreateServiceCategoryCommandHandler
        : ICommandHandler<CreateServiceCategoryCommand, CreateServiceCategoryResponse>
    {
        private readonly IGenericRepository<ServiceCategory> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;

        public CreateServiceCategoryCommandHandler(
            IGenericRepository<ServiceCategory> repository,
            IUnitOfWork unitOfWork,
            IUserContext userContext)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _userContext = userContext;
        }

        public async Task<Result<CreateServiceCategoryResponse>> Handle(
            CreateServiceCategoryCommand request,
            CancellationToken cancellationToken)
        {
            var category = ServiceCategory.Create(
                request.ServiceCategoryName,
                request.Description,
                request.ImagePath,
                _userContext.StaffId,
                _userContext.CompanyId,
                request.HasBranchPermission,
                request.AppSourceTypeId,
                request.Color,
                request.SortIndex);

            _repository.Add(category);

            var saveResult =
                await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (saveResult.IsFailure)
            {
                return Result<CreateServiceCategoryResponse>
                    .Failure(saveResult.Error);
            }

            return Result<CreateServiceCategoryResponse>.Success(
                new CreateServiceCategoryResponse(
                    category.ServiceCategoryId,
                    category.ServiceCategoryName));
        }
    }
}
