using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Products;
using Shared.Response;


namespace Application.Features.TenantFeatures.Products.Commands.ArchiveProduct;

public sealed class ArchiveProductCommandHandler
    : ICommandHandler<ArchiveProductCommand>
{
    private readonly ITenantRepository<Product> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public ArchiveProductCommandHandler(
        ITenantRepository<Product> repository,
        IUnitOfWork unitOfWork,
        IUserContext userContext)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<Result> Handle(
        ArchiveProductCommand request,
        CancellationToken cancellationToken)
    {
        // ------------------------------------------------------------
        // 1. Get Product
        // ------------------------------------------------------------

        var product =
            await _repository.FirstOrDefaultAsync(
                x =>
                    x.ProductId == request.ProductId &&
                    x.IsArchived!=true,
                cancellationToken:cancellationToken);

        if (product is null)
        {
            return Result.Failure(
                Error.NotFound("Product was not found."));
        }

        // ------------------------------------------------------------
        // 2. Archive
        // ------------------------------------------------------------

        product.Archive(_userContext.StaffId);

        // ------------------------------------------------------------
        // 3. Update Entity
        // ------------------------------------------------------------

        _repository.Update(product);

        // ------------------------------------------------------------
        // 4. Save
        // ------------------------------------------------------------

        var saveResult =
            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

        if (saveResult.IsFailure)
        {
            return Result.Failure(saveResult.Error);
        }

        return Result.Success();
    }
}
