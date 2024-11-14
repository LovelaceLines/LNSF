using LNSF.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LNSF.Infra.Data.Configurations;

public class NotificationConfiguration : BaseConfiguration<Notification>
{
	public override void Configure(EntityTypeBuilder<Notification> builder)
	{
		base.Configure(builder);

		builder.HasKey(n => n.Id);

		builder.Property(n => n.Title)
			.IsRequired()
			.HasMaxLength(100);

		builder.Property(n => n.Content)
			.IsRequired()
			.HasMaxLength(500);

		builder.Property(n => n.CreatedAt)
			.HasValueGenerator<DateTimeNowValueGenerator>();
	}
}
