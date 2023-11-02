using LNSF.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LNSF.Infra.Data.Configurations; 

public class HostingsConfiguration : IEntityTypeConfiguration<Hosting>
{
    public void Configure(EntityTypeBuilder<Hosting> builder)
    {
        builder.HasKey(h => h.Id);
        builder.Property(h => h.Id).ValueGeneratedOnAdd();
        builder.HasMany(h => h.Escorts).WithMany();
        builder.HasOne(h => h.Patient).WithMany().HasForeignKey(h => h.PatientId);
        builder.Property(h => h.CheckIn).IsRequired(true);
    }
}