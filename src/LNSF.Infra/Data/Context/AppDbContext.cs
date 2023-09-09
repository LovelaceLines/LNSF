using LNSF.Domain.Entities;
using LNSF.Infra.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace LNSF.Infra.Data.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<Tour> Tours { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<People> Peoples { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new ToursConfiguration());
        builder.ApplyConfiguration(new RoomsConfiguration());
        builder.ApplyConfiguration(new PeoplesConfiguration());
    }
}
