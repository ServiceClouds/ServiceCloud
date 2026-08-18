using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.ServiceEntities;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ServiceCategories.Commands.UpdateServiceCategory
{


    public sealed class UpdateServiceCategoryCommandHandler
        : ICommandHandler<UpdateServiceCategoryCommand, UpdateServiceCategoryResponse>
    {
        private readonly IGenericRepository<ServiceCategory> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;

        public UpdateServiceCategoryCommandHandler(
            IGenericRepository<ServiceCategory> repository,
            IUnitOfWork unitOfWork,
            IUserContext userContext)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _userContext = userContext;
        }

        public async Task<Result<UpdateServiceCategoryResponse>> Handle(
            UpdateServiceCategoryCommand request,
            CancellationToken cancellationToken)
        {
            var category = await _repository.FirstOrDefaultAsync(
                x => x.ServiceCategoryId == request.ServiceCategoryId &&
                     x.CompanyId == _userContext.CompanyId,
                asNoTracking: false,
                cancellationToken: cancellationToken);

            if (category is null)
            {
                return Result<UpdateServiceCategoryResponse>.Failure(
                    Error.NotFound("Service category not found."));
            }

            category.Update(
                request.ServiceCategoryName,
                request.Description,
                request.ImagePath,
                request.HasBranchPermission,
                request.Color,
                request.SortIndex,
                _userContext.StaffId);

            _repository.Update(category);

            var saveResult =
                await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (saveResult.IsFailure)
            {
                return Result<UpdateServiceCategoryResponse>
                    .Failure(saveResult.Error);
            }

            return Result<UpdateServiceCategoryResponse>.Success(
                new UpdateServiceCategoryResponse(
                    category.ServiceCategoryId,
                    category.ServiceCategoryName));
        }
    }
}