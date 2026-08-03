using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Persistence.Configurations.Master
{
    public class StaffTokenConfiguration : IEntityTypeConfiguration<StaffToken>
    {
        public void Configure(EntityTypeBuilder<StaffToken> entity)
        {
            entity.HasKey(e => e.StaffTokenId).HasName("PK__StaffTok__8321B1A8B21A6EEA");

            entity.ToTable("StaffToken");

            entity.Property(e => e.AccessTokenExpiry).HasColumnType("datetime");

            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.Property(e => e.RefreshToken).HasMaxLength(500);
            entity.Property(e => e.RefreshTokenExpiry).HasColumnType("datetime");
        }
    }
}