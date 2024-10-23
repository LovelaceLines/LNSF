using LNSF.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LNSF.Infra.Data.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
	public void Configure(EntityTypeBuilder<Role> builder)
	{
		builder.ToTable("Roles");

		builder.HasKey(r => r.Id);

		builder.Property(r => r.ConcurrencyStamp)
			.IsConcurrencyToken();

		builder.Property(p => p.CreatedAt)
			.ValueGeneratedOnAdd()
			.HasValueGenerator<DateTimeNowValueGenerator>();

		builder.Property(p => p.UpdatedAt)
			.ValueGeneratedOnUpdate()
			.HasValueGenerator<DateTimeNowValueGenerator>();

		builder.HasData(
			new Role
			{
				Id = 1,
				Name = "Desenvolvedor",
				NormalizedName = "DESENVOLVEDOR",
				ConcurrencyStamp = Guid.NewGuid().ToString(),
				CreatedAt = DateTime.Now,
				UpdatedAt = DateTime.Now
			},
			new Role
			{
				Id = 2,
				Name = "Administrador",
				NormalizedName = "ADMINISTRADOR",
				ConcurrencyStamp = Guid.NewGuid().ToString(),
				CreatedAt = DateTime.Now,
				UpdatedAt = DateTime.Now
			},
			new Role
			{
				Id = 3,
				Name = "AssistenteSocial",
				NormalizedName = "ASSISTENTESOCIAL",
				ConcurrencyStamp = Guid.NewGuid().ToString(),
				CreatedAt = DateTime.Now,
				UpdatedAt = DateTime.Now
			},
			new Role
			{
				Id = 4,
				Name = "Secretario",
				NormalizedName = "SECRETARIO",
				ConcurrencyStamp = Guid.NewGuid().ToString(),
				CreatedAt = DateTime.Now,
				UpdatedAt = DateTime.Now
			},
			new Role
			{
				Id = 5,
				Name = "Voluntario",
				NormalizedName = "VOLUNTARIO",
				ConcurrencyStamp = Guid.NewGuid().ToString(),
				CreatedAt = DateTime.Now,
				UpdatedAt = DateTime.Now
			}
		);
	}
}
