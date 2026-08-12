using MediatR;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Masterfeatures.Branches.Commands.Archive_Branch
{
    public sealed record ArchiveBranchCommand(
    int BranchId
) : IRequest<Result>;
}
