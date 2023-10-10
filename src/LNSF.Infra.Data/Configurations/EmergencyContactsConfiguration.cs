using LNSF.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LNSF.Infra.Data.Configurations;

public class EmergencyContactsConfiguration : IEntityTypeConfiguration<EmergencyContact>
{
    public void Configure(EntityTypeBuilder<EmergencyContact> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();
        
        builder.HasOne(x => x.People)
            .WithMany()
            .HasForeignKey(x => x.PeopleId);
        
        builder.Property(x => x.PeopleId)
            .IsRequired(true);
        
        builder.HasIndex(x => new { x.PeopleId, x.Phone })
            .IsUnique(true);
        
        builder.Property(x => x.Name)
            .IsRequired(true);
        
        builder.Property(x => x.Phone)
            .IsRequired(true);
    }
}
