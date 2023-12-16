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
            },
            new IdentityUserRole<string>
            {
                UserId = "2",
                RoleId = "2"
            },
            new IdentityUserRole<string>
            {
                UserId = "3",
                RoleId = "5"
            }
        );
    }
}
