using LNSF.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LNSF.Infra.Data.Configurations;

public class NotificationUserConfiguration : BaseConfiguration<NotificationUser>
{
	public override void Configure(EntityTypeBuilder<NotificationUser> builder)
	{
		base.Configure(builder);

		builder.HasKey(nu => nu.Id);

		builder.Property(nu => nu.ReadAt)
			.HasValueGenerator<DateTimeNowValueGenerator>();

		builder.HasOne(nu => nu.Notification)
			.WithMany()
			.HasForeignKey(nu => nu.NotificationId);

		builder.HasOne(nu => nu.User)
			.WithMany()
			.HasForeignKey(nu => nu.UserId);
	}
}
