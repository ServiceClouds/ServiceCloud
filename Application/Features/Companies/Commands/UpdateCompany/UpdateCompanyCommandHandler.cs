using Application.Abstractions.Data;
using Application.Common;
using Domain.Entities.ServiceCloud;
using MediatR;
using Persistence.Repositories.Common;
using Shared.Response;

namespace Application.Features.Companies.Commands.UpdateCompany;

public sealed class UpdateCompanyCommandHandler
    : IRequestHandler<UpdateCompanyCommand, Result>
{
    private readonly IGenericRepository<Company> _companyRepository;
    private readonly IMasterUnitOfWork  _unitOfWork;

    public UpdateCompanyCommandHandler(
        IGenericRepository<Company> companyRepository,
        IMasterUnitOfWork unitOfWork)
    {
        _companyRepository = companyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        UpdateCompanyCommand request,
        CancellationToken cancellationToken)
    {
        var company = await _companyRepository.FirstOrDefaultAsync(
            x => x.CompanyId == request.CompanyId &&
                 !x.IsArchived,
            cancellationToken: cancellationToken);

        if (company is null)
        {
            return Result.Failure(
                Error.NotFound("Company not found."));
        }

        company.Update(
            request.CompanyCode,
            request.CompanyName,
            request.CountryId,
            request.TimeZone,
            request.CurrencySymbol,
            request.ImagePath,
            modifiedBy: 1,
            request.AllowMigration,
            request.DatabaseConnectionCode,
            request.AccountTypeId);

        _companyRepository.Update(company);

        var saveResult = await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        if (saveResult.IsFailure)
        {
            return Result.Failure(saveResult.Error);
        }

        return Result.Success();
    }
}