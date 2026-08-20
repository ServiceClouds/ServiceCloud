using System;
using System.Collections.Generic;
using System.Text;

using Application.Abstractions.Commands;
namespace Application.Features.TenantFeatures.ProductBranchPermissions.Commands.DeactivateProductBranchPermission
{
    public sealed record DeactivateProductBranchPermissionCommand(
     int ProductBranchPermissionId
 ) : ICommand;
}
