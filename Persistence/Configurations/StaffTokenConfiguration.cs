using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Persistence.Configurations.Master
{
    public class StaffTokenConfiguration : IEntityTypeConfiguration<StaffToken>
    {
        public void Configure(EntityTypeBuilder<StaffToken> entity)
        {
            entity
                .HasNoKey()
                .ToTable("StaffToken");

            entity.Property(e => e.AccessTokenExpiry)
                .HasColumnType("datetime");

            entity.Property(e => e.AppSourceTypeId)
                .HasColumnName("AppSourceTypeID");

            entity.Property(e => e.CreatedOn)
                .HasColumnType("datetime");

            entity.Property(e => e.StaffLoginId)
                .HasColumnName("StaffLoginID");

            entity.Property(e => e.StaffTokenId)
                .ValueGeneratedOnAdd()
                .HasColumnName("StaffTokenID");
        }
    }
}