using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LNSF.Infra.Data.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
            new IdentityRole { Id = "1", Name = "Desenvolvedor", NormalizedName = "DESENVOLVEDOR", ConcurrencyStamp = Guid.NewGuid().ToString() },
            new IdentityRole { Id = "2", Name = "Administrador", NormalizedName = "ADMINISTRADOR", ConcurrencyStamp = Guid.NewGuid().ToString() },
            new IdentityRole { Id = "3", Name = "AssistenteSocial", NormalizedName = "ASSISTENTESOCIAL", ConcurrencyStamp = Guid.NewGuid().ToString() },
            new IdentityRole { Id = "4", Name = "Secretario", NormalizedName = "SECRETARIO", ConcurrencyStamp = Guid.NewGuid().ToString() },
            new IdentityRole { Id = "5", Name = "Voluntario", NormalizedName = "VOLUNTARIO", ConcurrencyStamp = Guid.NewGuid().ToString() }
        );
    }
}
