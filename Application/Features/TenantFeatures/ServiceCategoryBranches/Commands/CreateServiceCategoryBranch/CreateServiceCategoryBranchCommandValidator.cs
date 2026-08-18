using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ServiceCategoryBranches.Commands.CreateServiceCategoryBranch
{
    public sealed class CreateServiceCategoryBranchCommandValidator
    : AbstractValidator<CreateServiceCategoryBranchCommand>
    {
        public CreateServiceCategoryBranchCommandValidator()
        {
            RuleFor(x => x.ServiceCategoryId)
                .GreaterThan(0);

            RuleFor(x => x.BranchId)
                .GreaterThan(0);
        }
    }
}
