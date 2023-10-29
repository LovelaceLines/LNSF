using LNSF.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace LNSF.Infra.Data.Configurations;
public class EscortsConfiguration : IEntityTypeConfiguration<Escort>
{
    public void Configure(EntityTypeBuilder<Escort> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.HasOne(e => e.People).WithOne().HasForeignKey<Escort>(e => e.PeopleId);
        builder.Property(e => e.PeopleId).IsRequired();
    }
}