using Application.Common;
using MediatR;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ProductBranchPermissions.Queries.GetPagedProductBranchPermissions
{
    public sealed record GetPagedProductBranchPermissionsQuery(
    PaginationRequest Request
) : IRequest<Result<PagedResponse<ProductBranchPermissionResponse>>>;
}
