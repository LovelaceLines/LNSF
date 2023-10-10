using LNSF.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LNSF.Infra.Data.Configurations;

public class ToursConfiguration : IEntityTypeConfiguration<Tour>
{
    public void Configure(EntityTypeBuilder<Tour> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(x => x.People)
            .WithMany()
            .HasForeignKey(x => x.PeopleId);
        
        builder.Property(x => x.PeopleId)
            .IsRequired(true);
        
        builder.Property(x => x.Output)
            .IsRequired(true);
        
        builder.Property(x => x.Input)
            .IsRequired(false);
        
        builder.Property(x => x.Note)
            .IsRequired(false);
    }
}
