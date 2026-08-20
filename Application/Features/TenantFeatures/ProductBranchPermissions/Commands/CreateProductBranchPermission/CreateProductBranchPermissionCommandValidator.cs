using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ProductBranchPermissions.Commands.CreateProductBranchPermission
{
    public sealed class CreateProductBranchPermissionCommandValidator
    : AbstractValidator<CreateProductBranchPermissionCommand>
    {
        public CreateProductBranchPermissionCommandValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0);

            RuleFor(x => x.BranchId)
                .GreaterThan(0);
        }
    }
}
