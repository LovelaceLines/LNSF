using LNSF.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LNSF.Infra.Data.Configurations;

public class LogEntriesConfiguration : BaseConfiguration<LogEntry>
{
	public override void Configure(EntityTypeBuilder<LogEntry> builder)
	{
		base.Configure(builder);

		builder.HasKey(l => l.Id);

		builder.HasOne(l => l.User)
			.WithMany()
			.HasForeignKey(l => l.UserId);
	}
}
