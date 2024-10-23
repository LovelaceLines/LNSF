using LNSF.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LNSF.Infra.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.ToTable("Users");

		builder.HasKey(u => u.Id);

		builder.Property(u => u.UserName)
			.IsUnicode();

		builder.Property(p => p.CreatedAt)
			.ValueGeneratedOnAdd()
			.HasValueGenerator<DateTimeNowValueGenerator>();

		builder.Property(p => p.UpdatedAt)
			.ValueGeneratedOnUpdate()
			.HasValueGenerator<DateTimeNowValueGenerator>();

		builder.HasData(
			new User
			{
				Id = 1,
				Name = "George Maia",
				UserName = "georgedev",
				NormalizedUserName = "GEORGEDEV",
				Email = "georgemaiaf@gmail.com",
				NormalizedEmail = "GEORGEMAIAF@GMAIL.COM",
				PhoneNumber = "(55) 88 9 9246-5315",
				PasswordHash = new PasswordHasher<User>().HashPassword(new User("georgedev"), "!23L6(bNi.22T71,%4vfR{<~tA.]"),
				SecurityStamp = Guid.NewGuid().ToString(),
				ConcurrencyStamp = Guid.NewGuid().ToString(),
				CreatedAt = DateTime.Now,
				UpdatedAt = DateTime.Now
			},
			new User
			{
				Id = 2,
				Name = "Lar Nossa Senhora de Fátima",
				UserName = "lnsf",
				NormalizedUserName = "LNSF",
				Email = "lnsf@gmail.com",
				NormalizedEmail = "LNSF@GMAIL.COM",
				PhoneNumber = "(11) 11 1 1111-1111",
				PasswordHash = new PasswordHasher<User>().HashPassword(new User("lnsf"), "123456"),
				SecurityStamp = Guid.NewGuid().ToString(),
				ConcurrencyStamp = Guid.NewGuid().ToString(),
				CreatedAt = DateTime.Now,
				UpdatedAt = DateTime.Now
			},
			new User
			{
				Id = 3,
				Name = "Lar Nossa Senhora de Fátima 2",
				UserName = "lnsf2",
				NormalizedUserName = "LNSF2",
				Email = "lnsf2@gmail.com",
				NormalizedEmail = "LNSF2@GMAIL.COM",
				PhoneNumber = "(22) 22 2 2222-2222",
				PasswordHash = new PasswordHasher<User>().HashPassword(new User("lnsf"), "123456"),
				SecurityStamp = Guid.NewGuid().ToString(),
				ConcurrencyStamp = Guid.NewGuid().ToString(),
				CreatedAt = DateTime.Now,
				UpdatedAt = DateTime.Now
			}
		);
	}
}
