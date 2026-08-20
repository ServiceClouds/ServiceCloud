using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Products;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ProductVariants.Commands.CreateProductVariant
{


    public sealed class CreateProductVariantCommandHandler
        : ICommandHandler<CreateProductVariantCommand, CreateProductVariantResponse>
    {
        private readonly ITenantRepository<ProductVariant> _repository;
        private readonly ITenantRepository<Product> _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;

        public CreateProductVariantCommandHandler(
            ITenantRepository<ProductVariant> repository,
            ITenantRepository<Product> productRepository,
            IUnitOfWork unitOfWork,
            IUserContext userContext)
        {
            _repository = repository;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _userContext = userContext;
        }

        public async Task<Result<CreateProductVariantResponse>> Handle(
            CreateProductVariantCommand request,
            CancellationToken cancellationToken)
        {
            var productExists =
                await _productRepository.ExistsAsync(
                    x =>
                        x.ProductId == request.ProductId &&
                        x.IsArchived != true,
                    cancellationToken);

            if (!productExists)
            {
                return Result<CreateProductVariantResponse>.Failure(
                    Error.NotFound("Product was not found."));
            }

            var variantExists =
                await _repository.ExistsAsync(
                    x =>
                        x.ProductId == request.ProductId &&
                        x.ProductVariantName == request.ProductVariantName &&
                        !x.IsArchived,
                    cancellationToken);

            if (variantExists)
            {
                return Result<CreateProductVariantResponse>.Failure(
                    Error.Conflict(
                        "Product variant name already exists."));
            }

            var productVariant =
                ProductVariant.Create(
                    request.ProductId,
                    request.ProductVariantName,
                    _userContext.StaffId,
                    request.AttributeValueIds,
                    request.IsStandard,
                    request.SortedAttributeIds,
                    request.SortedAttributeValueIds);

            _repository.Add(productVariant);

            var saveResult =
                await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (saveResult.IsFailure)
            {
                return Result<CreateProductVariantResponse>
                    .Failure(saveResult.Error);
            }

            return Result<CreateProductVariantResponse>.Success(
                new CreateProductVariantResponse(
                    productVariant.ProductVariantId,
                    productVariant.ProductId,
                    productVariant.ProductVariantName));
        }
    }
}