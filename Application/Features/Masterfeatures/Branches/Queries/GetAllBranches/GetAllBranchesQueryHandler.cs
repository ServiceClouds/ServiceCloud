using MediatR;
using Persistence.Repositories.Common;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities.ServiceCloud;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Masterfeatures.Branches.Queries.GetAllBranches
{
    public sealed class GetAllBranchesQueryHandler
        : IRequestHandler<GetAllBranchesQuery, Result<List<Branch>>>
    {
        private readonly IGenericRepository<Branch> _branchRepository;

        public GetAllBranchesQueryHandler(
            IGenericRepository<Branch> branchRepository)
        {
            _branchRepository = branchRepository;
        }

        public async Task<Result<List<Branch>>> Handle(
            GetAllBranchesQuery request,
            CancellationToken cancellationToken)
        {
            var branches = await _branchRepository
                .GetAll()
                .Where(x => !x.IsArchived)
                .ToListAsync(cancellationToken);

            return Result<List<Branch>>.Success(branches);
        }
    }
}
