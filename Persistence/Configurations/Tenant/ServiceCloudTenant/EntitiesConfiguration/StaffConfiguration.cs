using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Tenant.ServiceCloudTenant.Entities;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Configurations.Tenant.ServiceCloudTenant.EntitiesConfiguration
{
    public class StaffConfiguration : IEntityTypeConfiguration<Staff>
    {
        public void Configure(EntityTypeBuilder<Staff> entity)
        {
            entity.Property(e => e.FullName)
                .HasComputedColumnSql("(Trim(([FirstName]+' ')+[LastName]))", true);

            entity.HasOne(d => d.Company)
                .WithMany(p => p.Staff)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Staff_Company");

            entity.HasOne(d => d.Country)
                .WithMany(p => p.Staff)
                .HasConstraintName("FK_Staff_Country");

            entity.HasOne(d => d.StaffPosition)
                .WithMany(p => p.Staff)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Staff_StaffPosition");

            entity.HasOne(d => d.StateCountry)
                .WithMany(p => p.Staff)
                .HasConstraintName("FK_Staff_StateCountry");
        }
    }
}
