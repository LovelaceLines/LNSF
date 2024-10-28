using LNSF.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LNSF.Infra.Data.Configurations;

public class HostingsEscortsConfiguration : BaseConfiguration<HostingEscort>
{
    public override void Configure(EntityTypeBuilder<HostingEscort> builder)
    {
        base.Configure(builder);

        builder.HasKey(he => new { he.HostingId, he.EscortId });

        builder.HasOne(he => he.Hosting)
            .WithMany()
            .HasForeignKey(he => he.HostingId);

        builder.HasOne(he => he.Escort)
            .WithMany()
            .HasForeignKey(he => he.EscortId);
    }
}
