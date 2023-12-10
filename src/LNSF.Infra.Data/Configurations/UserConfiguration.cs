using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserConfiguration : IEntityTypeConfiguration<IdentityUser>
{
    public void Configure(EntityTypeBuilder<IdentityUser> builder)
    {
        builder.HasData(
            new IdentityUser
            {
                Id = "1",
                UserName = "GeorgeMaia",
                NormalizedUserName = "GEORGEMAIA",
                Email = "georgemaiaf@gmail.com",
                NormalizedEmail = "GEORGEMAIAF@GMAIL.COM",
                PhoneNumber = "(55) 88 9 9246-5315",
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(new IdentityUser("GeorgeMaia"), "DevPass@123"),
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            }
        );
    }
}
