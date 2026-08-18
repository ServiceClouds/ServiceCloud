using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Entities;
using Shared.Response;

namespace Application.Features.TenantFeatures.Companies.Commands.ArchiveCompany;

public sealed class ArchiveCompanyCommandHandler
    : ICommandHandler<ArchiveCompanyCommand>
{
    private readonly ITenantRepository<Company> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public ArchiveCompanyCommandHandler(
        ITenantRepository<Company> repository,
        IUnitOfWork unitOfWork,
        IUserContext userContext)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<Result> Handle(
        ArchiveCompanyCommand request,
        CancellationToken cancellationToken)
    {
        // ------------------------------------------------------------
        // 1. Find active Company
        // ------------------------------------------------------------

        var company =
            await _repository.FirstOrDefaultAsync(
                x =>
                    x.CompanyId == request.CompanyId &&
                    x.IsActive,
                asNoTracking: false,
                cancellationToken);

        if (company is null)
        {
            return Result.Failure(
                Error.NotFound("Company not found."));
        }

        // ------------------------------------------------------------
        // 2. Archive through Domain Entity
        // ------------------------------------------------------------

        company.Archive(_userContext.StaffId);

        // ------------------------------------------------------------
        // 3. Update
        // ------------------------------------------------------------

        _repository.Update(company);

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