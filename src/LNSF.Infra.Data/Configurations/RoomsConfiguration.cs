using LNSF.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LNSF.Infra.Data.Configurations;

public class RoomsConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();
        
        builder.HasIndex(x => x.Number)
            .IsUnique();
        
        builder.Property(x => x.Number)
            .IsRequired();

        builder.Property(x => x.Bathroom)
            .IsRequired();

        builder.Property(x => x.Beds)
            .IsRequired();

        builder.Property(x => x.Storey)
            .IsRequired();
        
        builder.Property(x => x.Available)
            .IsRequired();
    }
}
