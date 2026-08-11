using Application.Common;
using MediatR;
using Persistence.Repositories.Common;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities.ServiceCloud;

namespace Application.Features.Companies.Commands.CreateCompany
{
    public sealed class CreateCompanyCommandHandler
    : IRequestHandler<CreateCompanyCommand, Result<int>>
    {
        private readonly IGenericRepository<Company> _companyRepository;
        private readonly IMasterUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;

        public CreateCompanyCommandHandler(
            IGenericRepository<Company> companyRepository,
            IMasterUnitOfWork unitOfWork,
            IUserContext userContext)
        {
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;
            _userContext = userContext;
        }

        public async Task<Result<int>> Handle(
            CreateCompanyCommand request,
            CancellationToken cancellationToken)
        {
            var exists = await _companyRepository.ExistsAsync(
                x => x.CompanyCode == request.CompanyCode,
                cancellationToken);

            if (exists)
            {
                return Result<int>.Failure(
                    Error.Conflict("Company with this code already exists."));
            }

            var company = Company.Create(
                companyCode: request.CompanyCode,
                companyName: request.CompanyName,
                countryId: request.CountryId,
                timeZone: request.TimeZone,
                currencySymbol: request.CurrencySymbol,
                imagePath: request.ImagePath,
                createdBy: 1,
                
                allowMigration: request.AllowMigration,
                databaseConnectionCode: request.DatabaseConnectionCode,
                accountTypeId: request.AccountTypeId);

            _companyRepository.Add(company);

            var saveResult = await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            if (saveResult.IsFailure)
            {
                return Result<int>.Failure(saveResult.Error);
            }

            return Result<int>.Success(company.CompanyId);
        }
    }
}
