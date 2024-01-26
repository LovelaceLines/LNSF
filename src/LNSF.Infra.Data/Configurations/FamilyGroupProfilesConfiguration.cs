using LNSF.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LNSF.Infra.Data.Configurations;

public class FamilyGroupProfilesConfiguration : IEntityTypeConfiguration<FamilyGroupProfile>
{
    public void Configure(EntityTypeBuilder<FamilyGroupProfile> builder)
    {
        builder.HasKey(f => f.Id);

        builder.Property(f => f.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(f => f.Patient)
            .WithMany()
            .HasForeignKey(f => f.PatientId);
    }
}
