using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LNSF.Infra.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<IdentityUser>
{
    public void Configure(EntityTypeBuilder<IdentityUser> builder)
    {
        // builder.HasData(new IdentityUser { Id = "1", UserName = "dev", ConcurrencyStamp = Guid.NewGuid().ToString() });
    }
}
