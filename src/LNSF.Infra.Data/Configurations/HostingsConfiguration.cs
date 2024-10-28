using LNSF.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LNSF.Infra.Data.Configurations;

public class HostingsConfiguration : BaseConfiguration<Hosting>
{
    public override void Configure(EntityTypeBuilder<Hosting> builder)
    {
        base.Configure(builder);

        builder.HasKey(h => h.Id);

        builder.Property(h => h.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(h => h.Patient)
            .WithMany()
            .HasForeignKey(h => h.PatientId);

        builder.Property(h => h.CheckIn)
            .IsRequired();

        builder.Property(h => h.CheckOut)
            .IsRequired(false);
    }
}