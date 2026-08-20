using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Products;
using Shared.Response;

namespace Application.Features.TenantFeatures.ProductVariants.Commands.ArchiveProductVariant;

public sealed class ArchiveProductVariantCommandHandler:ICommandHandler<ArchiveProductVariantCommand>
{
    private readonly ITenantRepository<ProductVariant> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public ArchiveProductVariantCommandHandler(
        ITenantRepository<ProductVariant> repository,
        IUnitOfWork unitOfWork,
        IUserContext userContext)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<Result> Handle(
        ArchiveProductVariantCommand request,
        CancellationToken cancellationToken)
    {
        var productVariant =
            await _repository.FirstOrDefaultAsync(
                x =>
                    x.ProductVariantId == request.ProductVariantId &&
                    !x.IsArchived,
                cancellationToken: cancellationToken);

        if (productVariant is null)
        {
            return Result.Failure(
                Error.NotFound("Product variant was not found."));
        }

        productVariant.Archive(_userContext.StaffId);

        _repository.Update(productVariant);

        var saveResult =
            await _unitOfWork.SaveChangesAsync(cancellationToken);

        if (saveResult.IsFailure)
        {
            return Result.Failure(saveResult.Error);
        }

        return Result.Success();
    }
}