using System;
using System.Collections.Generic;
using System.Text;
using Application.Abstractions.Commands;


namespace Application.Features.TenantFeatures.ServiceCategories.Commands.ArchiveServiceCategory
{
    public sealed record ArchiveServiceCategoryCommand(
    int ServiceCategoryId
) : ICommand;
}
