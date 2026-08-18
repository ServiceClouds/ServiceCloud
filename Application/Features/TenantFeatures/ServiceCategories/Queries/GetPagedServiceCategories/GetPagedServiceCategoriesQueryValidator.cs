using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ServiceCategories.Queries.GetPagedServiceCategories
{
    public sealed class GetPagedServiceCategoriesQueryValidator
     : AbstractValidator<GetPagedServiceCategoriesQuery>
    {
        public GetPagedServiceCategoriesQueryValidator()
        {
            RuleFor(x => x.Request.PageNumber)
                .GreaterThan(0);

            RuleFor(x => x.Request.PageSize)
                .GreaterThan(0);
        }
    }
}
