using Application.Common;
using Domain.Entities.ServiceCloud;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Abstractions.Repositories.Common;
using Shared.Response;

namespace Application.Features.Masterfeatures.Branches.Queries.GetPagedBranches
{
    public sealed class GetPagedBranchesQueryHandler
    : IRequestHandler<
        GetPagedBranchesQuery,
        Result<PagedResponse<Branch>>>
    {
        private readonly IMasterRepository<Branch> _branchRepository;

        public GetPagedBranchesQueryHandler(
            IMasterRepository<Branch> branchRepository)
        {
            _branchRepository = branchRepository;
        }

        public async Task<Result<PagedResponse<Branch>>> Handle(
            GetPagedBranchesQuery request,
            CancellationToken cancellationToken)
        {
            IQueryable<Branch> query = _branchRepository
                .GetAll()
                .Where(x => !x.IsArchived);

            // Search
            if (!string.IsNullOrWhiteSpace(request.Request.Search))
            {
                query = query.Where(x =>
                    x.BranchCode.Contains(request.Request.Search) ||
                    x.BranchName.Contains(request.Request.Search));
            }

            // Total records BEFORE pagination
            var totalRecords = await query.CountAsync(
                cancellationToken);

            // Pagination
            var branches = await query
                .Skip(
                    (request.Request.PageNumber - 1)
                    * request.Request.PageSize)
                .Take(request.Request.PageSize)
                .ToListAsync(cancellationToken);

            var response = new PagedResponse<Branch>
            {
                Items = branches,
                PageNumber = request.Request.PageNumber,
                PageSize = request.Request.PageSize,
                TotalRecords = totalRecords,
                TotalPages =
                    (int)Math.Ceiling(
                        (double)totalRecords /
                        request.Request.PageSize)
            };

            return Result<PagedResponse<Branch>>.Success(response);
        }
    }
}
