using Application.Features.TenantFeatures.ProductBranchPermissions.Queries.GetProductBranchPermissionById;
using MediatR;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;
using Application.Features.TenantFeatures.ProductBranchPermissions;

namespace Application.Features.TenantFeatures.ProductBranchPermissions.Queries.GetAllProductBranchPermissions
{

    public sealed record GetAllProductBranchPermissionsQuery
        : IRequest<Result<List<ProductBranchPermissionResponse>>>;
}
