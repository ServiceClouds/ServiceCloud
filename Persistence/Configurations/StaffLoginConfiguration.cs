using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Entities;

namespace Persistence.Data.Configurations
{
    public class StaffLoginConfiguration : IEntityTypeConfiguration<StaffLogin>
    {
        public void Configure(EntityTypeBuilder<StaffLogin> entity)
        {
            entity.HasKey(e => e.StaffLoginId).HasName("PK__StaffLog__C438A0373833115D");

            entity.ToTable("StaffLogin");

            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.LastLoggedInTime).HasColumnType("datetime");
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            entity.Property(e => e.PasswordExpiry).HasColumnType("datetime");
            entity.Property(e => e.PasswordHash).HasMaxLength(500);
            entity.Property(e => e.PasswordTokenExpiry).HasColumnType("datetime");
            entity.Property(e => e.Salt).HasMaxLength(500);
            entity.Property(e => e.SuspendedTill).HasColumnType("datetime");
        }
    }
}