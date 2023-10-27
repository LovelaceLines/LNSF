using LNSF.Domain.Entities;
using LNSF.Infra.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace LNSF.Infra.Data.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<People> Peoples { get; set; }
    public DbSet<Tour> Tours { get; set; }
    public DbSet<EmergencyContact> EmergencyContacts { get; set; }
    public DbSet<Hospital> Hospitals { get; set; }
    public DbSet<Patient> Patients { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new AccountsConfiguration());
        builder.ApplyConfiguration(new RoomsConfiguration());
        builder.ApplyConfiguration(new PeoplesConfiguration());
        builder.ApplyConfiguration(new ToursConfiguration());
        builder.ApplyConfiguration(new EmergencyContactsConfiguration());
        builder.ApplyConfiguration(new HospitalsConfiguration());
        builder.ApplyConfiguration(new PatientsConfiguration());
    }
}