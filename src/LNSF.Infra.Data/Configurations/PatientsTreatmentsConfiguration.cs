using LNSF.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LNSF.Infra.Data.Configurations;

public class PatientsTreatmentsConfiguration : BaseConfiguration<PatientTreatment>
{
    public override void Configure(EntityTypeBuilder<PatientTreatment> builder)
    {
        base.Configure(builder);

        builder.HasKey(pt => new { pt.PatientId, pt.TreatmentId });

        builder.HasOne(pt => pt.Patient)
            .WithMany()
            .HasForeignKey(pt => pt.PatientId);

        builder.HasOne(pt => pt.Treatment)
            .WithMany()
            .HasForeignKey(pt => pt.TreatmentId);
    }
}
