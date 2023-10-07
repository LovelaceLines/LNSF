using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LNSF.Infra.Data.Migrations;

public class HelperMigration
{
    public HelperMigration(IServiceProvider serviceProvider)
    {
        var scope = serviceProvider.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        dbContext.Database.Migrate();
    }
}
