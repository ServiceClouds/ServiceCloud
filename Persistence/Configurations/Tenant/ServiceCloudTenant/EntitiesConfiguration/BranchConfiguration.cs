using Domain.Tenant.ServiceCloudTenant.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Configurations.Tenant.ServiceCloudTenant.EntitiesConfiguration
{
    public class BranchConfiguration : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> entity)
        {
            entity.HasOne(d => d.Company)
                .WithMany(p => p.Branches)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Branch_Company");

            entity.HasOne(d => d.Country)
                .WithMany(p => p.Branches)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Branch_Country");
        }
    }
}
