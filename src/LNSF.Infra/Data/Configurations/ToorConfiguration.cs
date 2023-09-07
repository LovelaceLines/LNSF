using LNSF.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LNSF.Infra.Data.Configurations;

public class ToorConfiguration : IEntityTypeConfiguration<Toor>
{
    public void Configure(EntityTypeBuilder<Toor> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();
        
        builder.Property(e => e.Output)
            .IsRequired(false);
        
        builder.Property(e => e.Input)
            .IsRequired(false);
        
        builder.Property(e => e.Note)
            .IsRequired(false);
    }
}
