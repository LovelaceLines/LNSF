using LNSF.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LNSF.Infra.Data.Configurations;

public class PeoplesConfiguration : BaseConfiguration<People>
{
	public override void Configure(EntityTypeBuilder<People> builder)
	{
		base.Configure(builder);

		builder.HasKey(x => x.Id);

		builder.Property(x => x.Id)
			.ValueGeneratedOnAdd();

		builder.HasIndex(x => x.RG)
			.IsUnique()
			.HasFilter(null);

		builder.HasIndex(x => x.CPF)
			.IsUnique()
			.HasFilter(null);

		builder.Property(e => e.Name)
			.IsRequired();
	}
}
