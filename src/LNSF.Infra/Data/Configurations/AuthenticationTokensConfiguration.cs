using LNSF.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LNSF.Infra.Data.Configurations;

public class AuthenticationTokensConfiguration : IEntityTypeConfiguration<AuthenticationToken>
{
    public void Configure(EntityTypeBuilder<AuthenticationToken> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(x => x.Account)
            .WithOne()
            .HasForeignKey<AuthenticationToken>(x => x.AccountId);
    }
}
