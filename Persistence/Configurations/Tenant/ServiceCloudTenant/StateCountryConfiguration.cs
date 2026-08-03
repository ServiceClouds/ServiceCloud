using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Tenant.ServiceCloudTenant.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Configurations.Tenant.ServiceCloudTenant
{
    public class StateCountryConfiguration : IEntityTypeConfiguration<StateCountry>
    {
        public void Configure(EntityTypeBuilder<StateCountry> entity)
        {
            entity.HasOne(d => d.Country)
                .WithMany(p => p.StateCountries)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StateCountry_Country");
        }
    }
}
