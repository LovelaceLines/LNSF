using LNSF.Domain.Entities;
using LNSF.Infra.Data.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LNSF.Infra.Data.Context;

public class AppDbContext : IdentityDbContext<IdentityUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<People> Peoples { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<PeopleRoom> PeoplesRooms { get; set; }
    public DbSet<Tour> Tours { get; set; }
    public DbSet<EmergencyContact> EmergencyContacts { get; set; }
    public DbSet<Hospital> Hospitals { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Escort> Escorts { get; set; }
    public DbSet<Treatment> Treatments { get; set; }
    public DbSet<Hosting> Hostings { get; set; }
    public DbSet<PatientTreatment> PatientsTreatments { get; set; }
    public DbSet<HostingEscort> HostingsEscorts { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new PeoplesConfiguration());
        builder.ApplyConfiguration(new RoomsConfiguration());
        builder.ApplyConfiguration(new PeoplesRoomsConfiguration());
        builder.ApplyConfiguration(new ToursConfiguration());
        builder.ApplyConfiguration(new EmergencyContactsConfiguration());
        builder.ApplyConfiguration(new HospitalsConfiguration());
        builder.ApplyConfiguration(new PatientsConfiguration());
        builder.ApplyConfiguration(new EscortsConfiguration());
        builder.ApplyConfiguration(new TreatmentsConfiguration());
        builder.ApplyConfiguration(new HostingsConfiguration());
        builder.ApplyConfiguration(new PatientsTreatmentsConfiguration());
        builder.ApplyConfiguration(new HostingsEscortsConfiguration());
        builder.ApplyConfiguration(new RoleConfiguration());
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new UserRoleConfiguration());
    }
}