using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Persistence.Data.Configurations
{
    public class StaffBranchConfiguration : IEntityTypeConfiguration<StaffBranch>
    {
        public void Configure(EntityTypeBuilder<StaffBranch> entity)
        {
            entity.HasKey(e => e.StaffBranchId).HasName("PK__StaffBra__FCCE39AC4F4BF914");

            entity.ToTable("StaffBranch");

            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        }
    }
}