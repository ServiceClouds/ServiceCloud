using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ProductBranchPermissions.Commands.DeactivateProductBranchPermission
{
    public sealed class DeactivateProductBranchPermissionCommandValidator
    : AbstractValidator<DeactivateProductBranchPermissionCommand>
    {
        public DeactivateProductBranchPermissionCommandValidator()
        {
            RuleFor(x => x.ProductBranchPermissionId)
                .GreaterThan(0);
        }
    }
}
