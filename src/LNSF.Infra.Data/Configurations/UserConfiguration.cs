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
                UserName = "georgedev",
                NormalizedUserName = "GEORGEDEV",
                Email = "georgemaiaf@gmail.com",
                NormalizedEmail = "GEORGEMAIAF@GMAIL.COM",
                PhoneNumber = "(55) 88 9 9246-5315",
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(new IdentityUser("georgedev"), "!23L6(bNi.22T71,%4vfR{<~tA.]"),
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            },
            new IdentityUser
            {
                Id = "2",
                UserName = "lnsf",
                NormalizedUserName = "LNSF",
                Email = "lnsf@gmail.com",
                NormalizedEmail = "LNSF@GMAIL.COM",
                PhoneNumber = "(11) 11 1 1111-1111",
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(new IdentityUser("lnsf"), "123456"),
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            },
            new IdentityUser
            {
                Id = "3",
                UserName = "lnsf2",
                NormalizedUserName = "LNSF2",
                Email = "lnsf2@gmail.com",
                NormalizedEmail = "LNSF2@GMAIL.COM",
                PhoneNumber = "(22) 22 2 2222-2222",
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(new IdentityUser("lnsf"), "123456"),
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            }
        );
    }
}
