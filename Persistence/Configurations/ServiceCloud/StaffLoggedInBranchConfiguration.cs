using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities.ServiceCloud;

namespace Persistence.Configurations.ServiceCloud
{
    public class StaffLoggedInBranchConfiguration : IEntityTypeConfiguration<StaffLoggedInBranch>
    {
        public void Configure(EntityTypeBuilder<StaffLoggedInBranch> entity)
        {
            entity.HasKey(e => e.StaffTokenId).HasName("PK__StaffLog__8321B1A8202DF456");

            entity.ToTable("StaffLoggedInBranch");

            entity.Property(e => e.LoggedInDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        }
    }
}