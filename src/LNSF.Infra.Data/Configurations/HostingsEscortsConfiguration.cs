using LNSF.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LNSF.Infra.Data.Configurations;

public class HostingsEscortsConfiguration : IEntityTypeConfiguration<HostingEscort>
{
    public void Configure(EntityTypeBuilder<HostingEscort> builder)
    {
        builder.HasKey(he => new { he.HostingId, he.EscortId });

        builder.HasOne(he => he.Hosting)
            .WithMany()
            .HasForeignKey(he => he.HostingId);

        builder.HasOne(he => he.Escort)
            .WithMany()
            .HasForeignKey(he => he.EscortId);
        
        builder.Property(he => he.CheckIn)
            .IsRequired();
    }
}
