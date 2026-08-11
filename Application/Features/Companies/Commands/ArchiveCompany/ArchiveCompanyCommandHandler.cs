using Application.Abstractions.Data;
using Application.Common;
using Domain.Entities.ServiceCloud;
using MediatR;
using Persistence.Repositories.Common;
using Shared.Response;

namespace Application.Features.Companies.Commands.ArchiveCompany;

public sealed class ArchiveCompanyCommandHandler
    : IRequestHandler<ArchiveCompanyCommand, Result>
{
    private readonly IGenericRepository<Company> _companyRepository;
    private readonly IMasterUnitOfWork  _unitOfWork;

    public ArchiveCompanyCommandHandler(
        IGenericRepository<Company> companyRepository,
        IMasterUnitOfWork  unitOfWork)
    {
        _companyRepository = companyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        ArchiveCompanyCommand request,
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

        company.Archive(modifiedBy: 1);

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