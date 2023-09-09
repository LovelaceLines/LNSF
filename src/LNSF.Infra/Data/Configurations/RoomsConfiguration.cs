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
        
        builder.Property(x => x.Number)
            .IsRequired(true);

        builder.Property(x => x.Bathroom)
            .IsRequired(true);

        builder.Property(x => x.Beds)
            .IsRequired(true);

        builder.Property(x => x.Occupation)
            .IsRequired(true);

        builder.Property(x => x.Storey)
            .IsRequired(true);
        
        builder.Property(x => x.Available)
            .IsRequired(true);
    }
}
