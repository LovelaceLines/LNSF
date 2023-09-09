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
            .WithMany(x => x.People)
            .HasForeignKey(x => x.RoomId);
    }
}
