using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Persistence.Data.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> entity)
        {
            entity.HasKey(e => e.CompanyId).HasName("PK__Company__2D971CAC33A0AB20");

            entity.ToTable("Company");

            entity.Property(e => e.CompanyCode).HasMaxLength(50);
            entity.Property(e => e.CompanyName).HasMaxLength(200);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CurrencySymbol).HasMaxLength(10);
            entity.Property(e => e.DatabaseConnectionCode).HasMaxLength(128);
            entity.Property(e => e.ImagePath)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            entity.Property(e => e.TimeZone)
                .HasMaxLength(100)
                .IsUnicode(false);
        }
    }
}