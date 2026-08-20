using MediatR;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ProductBranchPermissions.Queries.GetProductBranchPermissionById
{
    public sealed record GetProductBranchPermissionByIdQuery(
       int ProductBranchPermissionId
   ) : IRequest<Result<ProductBranchPermissionResponse>>;
}
