using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ProductBranchPermissions.Commands.UpdateProductBranchPermission
{
    public sealed class UpdateProductBranchPermissionCommandValidator
    : AbstractValidator<UpdateProductBranchPermissionCommand>
    {
        public UpdateProductBranchPermissionCommandValidator()
        {
            RuleFor(x => x.ProductBranchPermissionId)
                .GreaterThan(0);
        }
    }
}
