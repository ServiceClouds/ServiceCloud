using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Tenant.ServiceCloudTenant.Entities;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Configurations.Tenant.ServiceCloudTenant.EntitiesConfiguration
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> entity)
        {
            entity.HasOne(d => d.Country)
                .WithMany(p => p.Companies)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Company_Country");

            entity.HasOne(d => d.Currency)
                .WithMany(p => p.Companies)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Company_Currency");
        }
    }
}
