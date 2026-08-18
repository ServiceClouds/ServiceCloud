using Application.Common;
using MediatR;

using Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities.ServiceCloud;
using Application.Abstractions.Repositories.Common;

namespace Application.Features.Masterfeatures.Branches.Commands.UpdateBranch
{
    public sealed class UpdateBranchCommandHandler
    : IRequestHandler<UpdateBranchCommand, Result>
    {
        private readonly IMasterRepository<Branch> _branchRepository;
        private readonly IMasterUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;

        public UpdateBranchCommandHandler(
            IMasterRepository<Branch> branchRepository,
            IMasterUnitOfWork unitOfWork,
            IUserContext userContext)
        {
            _branchRepository = branchRepository;
            _unitOfWork = unitOfWork;
            _userContext = userContext;
        }

        public async Task<Result> Handle(
            UpdateBranchCommand request,
            CancellationToken cancellationToken)
        {
            var branch = await _branchRepository.FirstOrDefaultAsync(
                x => x.BranchId == request.BranchId &&
                     !x.IsArchived,
                cancellationToken: cancellationToken);

            if (branch is null)
            {
                return Result.Failure(
                    Error.NotFound("Branch not found."));
            }

            branch.Update(
                branchCode: request.BranchCode,
                countryId: request.CountryId,
                modifiedBy: 1,
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

            _branchRepository.Update(branch);

            var saveResult = await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            if (saveResult.IsFailure)
            {
                return Result.Failure(saveResult.Error);
            }

            return Result.Success();
        }
    }
    }
