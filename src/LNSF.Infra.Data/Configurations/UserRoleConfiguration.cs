using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LNSF.Infra.Data.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(
            new IdentityUserRole<string>
            {
                UserId = "1",
                RoleId = "1"
            }
        );
    }
}
