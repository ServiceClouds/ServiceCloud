using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Tenant.ServiceCloudTenant.Entities;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Configurations.Tenant.ServiceCloudTenant.EntitiesConfiguration
{
    public class StaffBranchConfiguration : IEntityTypeConfiguration<StaffBranch>
    {
        public void Configure(EntityTypeBuilder<StaffBranch> entity)
        {
            entity.HasOne(d => d.Branch)
                .WithMany(p => p.StaffBranches)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StaffBranch_Branch");

            entity.HasOne(d => d.Role)
                .WithMany(p => p.StaffBranches)
                .HasConstraintName("FK_StaffBranch_Role");

            entity.HasOne(d => d.Staff)
                .WithMany(p => p.StaffBranches)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StaffBranch_Staff");
        }
    }
}
