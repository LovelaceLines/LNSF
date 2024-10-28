using LNSF.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LNSF.Infra.Data.Configurations;

public class EmergencyContactsConfiguration : BaseConfiguration<EmergencyContact>
{
    public override void Configure(EntityTypeBuilder<EmergencyContact> builder)
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

        builder.HasIndex(x => new { x.PeopleId, x.Phone })
            .IsUnique();

        builder.Property(x => x.Name)
            .IsRequired();

        builder.Property(x => x.Phone)
            .IsRequired();
    }
}
