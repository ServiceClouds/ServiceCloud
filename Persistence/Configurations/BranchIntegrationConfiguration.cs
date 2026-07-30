using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Persistence.Data.Configurations
{
    public class BranchIntegrationConfiguration : IEntityTypeConfiguration<BranchIntegration>
    {
        public void Configure(EntityTypeBuilder<BranchIntegration> entity)
        {
            entity.HasKey(e => e.BranchId).HasName("PK__BranchIn__A1682FC5602D308F");

            entity.ToTable("BranchIntegration");

            entity.Property(e => e.BranchId).ValueGeneratedNever();
            entity.Property(e => e.GoCardlessOrganizationId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MailGunEmail)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TwilioMobile)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.TwilioPhoneNumberForVoice)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.WhatsAppPhoneNumber).HasMaxLength(200);
        }
    }
}