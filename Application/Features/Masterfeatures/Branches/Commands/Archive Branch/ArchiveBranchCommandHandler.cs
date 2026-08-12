using Application.Common;
using MediatR;
using Persistence.Repositories.Common;
using Shared.Response;
using Domain.Entities.ServiceCloud;

namespace Application.Features.Masterfeatures.Branches.Commands.Archive_Branch
{
    public sealed class ArchiveBranchCommandHandler
    : IRequestHandler<ArchiveBranchCommand, Result>
    {
        private readonly IGenericRepository<Branch> _branchRepository;
        private readonly IMasterUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;

        public ArchiveBranchCommandHandler(
            IGenericRepository<Branch> branchRepository,
            IMasterUnitOfWork unitOfWork,
            IUserContext userContext)
        {
            _branchRepository = branchRepository;
            _unitOfWork = unitOfWork;
            _userContext = userContext;
        }

        public async Task<Result> Handle(
            ArchiveBranchCommand request,
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

            branch.Archive(1);

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
