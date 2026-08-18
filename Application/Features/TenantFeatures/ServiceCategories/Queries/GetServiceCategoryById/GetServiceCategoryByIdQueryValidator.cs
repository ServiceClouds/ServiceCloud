using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ServiceCategories.Queries.GetServiceCategoryById
{
    public sealed class GetServiceCategoryByIdQueryValidator
    : AbstractValidator<GetServiceCategoryByIdQuery>
    {
        public GetServiceCategoryByIdQueryValidator()
        {
            RuleFor(x => x.ServiceCategoryId)
                .GreaterThan(0);
        }
    }
}
