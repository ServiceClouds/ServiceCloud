using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Entities;

namespace Persistence.Data.Configurations
{
    public class BranchConfiguration : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> entity)
        {
            entity.HasKey(e => e.BranchId).HasName("PK__Branch__A1682FC5975CD559");

            entity.ToTable("Branch");

            entity.Property(e => e.Address1).HasMaxLength(500);
            entity.Property(e => e.Address2).HasMaxLength(500);
            entity.Property(e => e.BranchCode).HasMaxLength(50);
            entity.Property(e => e.BranchName).HasMaxLength(200);
            entity.Property(e => e.CityName).HasMaxLength(100);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Currency).HasMaxLength(10);
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.Fax).HasMaxLength(20);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Mobile).HasMaxLength(20);
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            entity.Property(e => e.Phone1).HasMaxLength(20);
            entity.Property(e => e.PostalCode).HasMaxLength(20);
            entity.Property(e => e.PrivacyPolicyUrl)
                .HasMaxLength(500)
                .HasColumnName("PrivacyPolicyURL");
            entity.Property(e => e.StateCountyName).HasMaxLength(100);
            entity.Property(e => e.TermsOfServiceUrl)
                .HasMaxLength(500)
                .HasColumnName("TermsOfServiceURL");
            entity.Property(e => e.TimeZone).HasMaxLength(100);
        }
    }
}