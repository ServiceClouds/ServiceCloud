using MediatR;
using Application.Abstractions.Repositories.Common;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities.ServiceCloud;

namespace Application.Features.Masterfeatures.Branches.Queries.GetBranchById
{
    public sealed class GetBranchByIdQueryHandler
         : IRequestHandler<GetBranchByIdQuery, Result<Branch>>
    {
        private readonly IMasterRepository<Branch> _branchRepository;

        public GetBranchByIdQueryHandler(
            IMasterRepository<Branch> branchRepository)
        {
            _branchRepository = branchRepository;
        }

        public async Task<Result<Branch>> Handle(
            GetBranchByIdQuery request,
            CancellationToken cancellationToken)
        {
            var branch = await _branchRepository.FirstOrDefaultAsync(
                x => x.BranchId == request.BranchId &&
                     !x.IsArchived,
                cancellationToken: cancellationToken);

            if (branch is null)
            {
                return Result<Branch>.Failure(
                    Error.NotFound("Branch not found."));
            }

            return Result<Branch>.Success(branch);
        }
    }
}
