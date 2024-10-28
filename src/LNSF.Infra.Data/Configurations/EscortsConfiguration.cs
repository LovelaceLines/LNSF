using LNSF.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LNSF.Infra.Data.Configurations;

public class EscortsConfiguration : BaseConfiguration<Escort>
{
    public override void Configure(EntityTypeBuilder<Escort> builder)
    {
        base.Configure(builder);

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(e => e.People)
            .WithOne()
            .HasForeignKey<Escort>(e => e.PeopleId);

        builder.Property(e => e.PeopleId)
            .IsUnicode();
    }
}