using Domain.Entities.Tenant.ServiceCloudTenant.ServiceEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Abstractions.Data

{
        public interface IApplicationDbContext:IDbContext
    {
        //DbSet<T> GetDbSet<T>() where T : class;
        DbSet<Service> Services { get; }
        DbSet<ServiceCategory> ServiceCategories { get; }
        DbSet<ServiceCategoryBranch> ServiceCategoryBranches { get; }
    }
}
