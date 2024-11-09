using LNSF.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LNSF.Infra.Data.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
	public void Configure(EntityTypeBuilder<UserRole> builder)
	{
		builder.ToTable("UserRoles");

		builder.HasKey(ur => ur.Id);

		builder.HasIndex(ur => new { ur.UserId, ur.RoleId });

		builder.HasOne(ur => ur.Role)
			.WithMany()
			.HasForeignKey(ur => ur.RoleId);

		builder.HasOne(ur => ur.User)
			.WithMany()
			.HasForeignKey(ur => ur.UserId);

		builder.HasData(
			new UserRole
			{
				Id = 1,
				UserId = 1,
				RoleId = 1,
			},
			new UserRole
			{
				Id = 2,
				UserId = 2,
				RoleId = 2,
			},
			new UserRole
			{
				Id = 3,
				UserId = 3,
				RoleId = 3,
			},
			new UserRole
			{
				Id = 4,
				UserId = 4,
				RoleId = 4,
			},
			new UserRole
			{
				Id = 5,
				UserId = 5,
				RoleId = 5,
			}
		);
	}
}
