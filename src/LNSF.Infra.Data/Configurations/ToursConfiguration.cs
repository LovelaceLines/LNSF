using LNSF.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LNSF.Infra.Data.Configurations;

public class ToursConfiguration : BaseConfiguration<Tour>
{
    public override void Configure(EntityTypeBuilder<Tour> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(x => x.People)
            .WithMany()
            .HasForeignKey(x => x.PeopleId);

        builder.Property(x => x.PeopleId)
            .IsRequired();

        builder.Property(x => x.Output)
            .IsRequired();
    }
}
