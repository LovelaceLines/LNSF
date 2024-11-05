using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LNSF.Infra.Data.Configurations;

public abstract class BaseConfiguration<T> : IEntityTypeConfiguration<T> where T : class
{
	public virtual void Configure(EntityTypeBuilder<T> builder)
	{
		// builder.Property<DateTime?>("CreatedAt")
		//     .ValueGeneratedOnAdd()
		//     .HasValueGenerator<DateTimeNowValueGenerator>();

		// builder.Property<DateTime?>("UpdatedAt")
		//     .ValueGeneratedOnUpdate()
		//     .HasValueGenerator<DateTimeNowValueGenerator>();
	}
}
