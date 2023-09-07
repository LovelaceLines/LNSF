using LNSF.Domain.Entities;
using LNSF.Infra.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace LNSF.Infra.Data.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<Toor> Toors { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new ToorConfiguration());
    }
}
