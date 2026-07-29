using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Entities;

namespace Persistence.Data.Configurations
{
    public class StaffConfiguration : IEntityTypeConfiguration<Staff>
    {
        public void Configure(EntityTypeBuilder<Staff> entity)
        {
            entity.HasKey(e => e.StaffId).HasName("PK__Staff__96D4AB17A22BCDC6");

            entity.Property(e => e.Address1).HasMaxLength(500);
            entity.Property(e => e.Address2).HasMaxLength(500);
            entity.Property(e => e.AllowLogin).HasDefaultValue(true);
            entity.Property(e => e.CardNumber).HasMaxLength(50);
            entity.Property(e => e.CityName).HasMaxLength(100);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(20);
            entity.Property(e => e.ImagePath).HasMaxLength(500);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.Mobile).HasMaxLength(20);
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.PostCode).HasMaxLength(20);
            entity.Property(e => e.StateCountyName).HasMaxLength(100);
            entity.Property(e => e.Title).HasMaxLength(50);
        }
    }
}