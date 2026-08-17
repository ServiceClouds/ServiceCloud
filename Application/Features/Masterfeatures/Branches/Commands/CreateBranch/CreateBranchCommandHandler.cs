using Application.Common;
using MediatR;
using Shared.Response;
using Domain.Entities.ServiceCloud;
using Application.Abstractions.Repositories.Common;

namespace Application.Features.Masterfeatures.Branches.Commands.CreateBranch
{
    public sealed class CreateBranchCommandHandler
        : IRequestHandler<CreateBranchCommand, Result<int>>
    {
        private readonly IGenericRepository<Branch> _branchRepository;
        private readonly IMasterUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;

        public CreateBranchCommandHandler(
            IGenericRepository<Branch> branchRepository,
            IMasterUnitOfWork unitOfWork,
            IUserContext userContext)
        {
            _branchRepository = branchRepository;
            _unitOfWork = unitOfWork;
            _userContext = userContext;
        }

        public async Task<Result<int>> Handle(
            CreateBranchCommand request,
            CancellationToken cancellationToken)
        {
            var exists = await _branchRepository.ExistsAsync(
                x => x.BranchCode == request.BranchCode,
                cancellationToken);

            if (exists)
            {
                return Result<int>.Failure(
                    Error.Conflict("Branch with this code already exists."));
            }

            var branch = Branch.Create(
                branchCode: request.BranchCode,
                countryId: request.CountryId,
                companyId: request.CompanyId,
                createdBy: 1,

                branchName: request.BranchName,
                stateCountyName: request.StateCountyName,
                cityName: request.CityName,
                timeZone: request.TimeZone,
                currency: request.Currency,
                address1: request.Address1,
                address2: request.Address2,
                postalCode: request.PostalCode,
                email: request.Email,
                mobile: request.Mobile,
                phone1: request.Phone1,
                fax: request.Fax,
                isOnline: request.IsOnline,
                termsOfServiceUrl: request.TermsOfServiceUrl,
                privacyPolicyUrl: request.PrivacyPolicyUrl,
                dateFormatId: request.DateFormatId,
                isActive: request.IsActive);

            _branchRepository.Add(branch);

            var saveResult = await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            if (saveResult.IsFailure)
            {
                return Result<int>.Failure(saveResult.Error);
            }

            return Result<int>.Success(branch.BranchId);
        }
    }
}