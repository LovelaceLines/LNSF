using LNSF.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LNSF.Infra.Data.Configurations;

public class PeoplesRoomsConfiguration : IEntityTypeConfiguration<PeopleRoom>
{
    public void Configure(EntityTypeBuilder<PeopleRoom> builder)
    {
        builder.HasKey(pr => pr.Id);

        builder.Property(pr => pr.Id)
            .ValueGeneratedOnAdd();
        
        builder.Property(pr => pr.Occupation)
            .IsRequired();

        builder.HasOne(pr => pr.People)
            .WithMany()
            .HasForeignKey(pr => pr.PeopleId);

        builder.HasOne(pr => pr.Room)
            .WithMany()
            .HasForeignKey(pr => pr.RoomId);
    }
}
