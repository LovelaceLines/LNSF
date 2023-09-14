using LNSF.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LNSF.Infra.Data.Configurations;

public class PeoplesConfiguration : IEntityTypeConfiguration<People>
{
    public void Configure(EntityTypeBuilder<People> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();
        
        builder.HasOne(x => x.Room)
            .WithMany()
            .HasForeignKey(x => x.RoomId);
        
        builder.Property(x => x.RoomId)
            .IsRequired(false);
        
        builder.HasIndex(x => x.RG)
            .IsUnique(true);
        
        builder.HasIndex(x => x.CPF)
            .IsUnique(true);
        
        builder.Property(e => e.Name)
            .IsRequired(true);

        builder.Property(e => e.Street)
            .IsRequired(false);

        builder.Property(e => e.HouseNumber)
            .IsRequired(false);

        builder.Property(e => e.Neighborhood)
            .IsRequired(false);
        
        builder.Property(e => e.City)
            .IsRequired(false);

        builder.Property(e => e.State)
            .IsRequired(false);

        builder.Property(e => e.Phone)
            .IsRequired(false);

        builder.Property(e => e.Note)
            .IsRequired(false);
    }
}
