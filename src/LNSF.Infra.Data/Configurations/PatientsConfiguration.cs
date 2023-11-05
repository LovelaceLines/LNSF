using LNSF.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LNSF.Infra.Data.Configurations; 

public class PatientsConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(p => p.People)
            .WithOne()
            .HasForeignKey<Patient>(p => p.PeopleId);

        builder.HasOne(p => p.Hospital)
            .WithMany()
            .HasForeignKey(p => p.HospitalId);
        
        builder.Ignore(p => p.TreatmentIds);
    }
}