using LNSF.Domain.Entities;
using LNSF.Domain.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LNSF.Infra.Data.Configurations;

public class TreatmentsConfiguration : BaseConfiguration<Treatment>
{
	public override void Configure(EntityTypeBuilder<Treatment> builder)
	{
		base.Configure(builder);

		builder.HasKey(t => t.Id);

		builder.Property(t => t.Id)
			.ValueGeneratedOnAdd();

		builder.Property(t => t.Type)
			.IsRequired();

		builder.Property(t => t.Name)
			.IsRequired();

		builder.HasIndex(t => new { t.Name, t.Type })
			.IsUnique();

		builder.HasData(
			new Treatment
			{
				Id = 1,
				Name = "Câncer",
				Type = TypeTreatment.CANCER
			},
			new Treatment
			{
				Id = 2,
				Name = "Pré-transplante",
				Type = TypeTreatment.PRETRANSPLANT
			},
			new Treatment
			{
				Id = 3,
				Name = "Pós-transplante",
				Type = TypeTreatment.POSTTRANSPLANT
			},
			new Treatment
			{
				Id = 4,
				Name = "Outro",
				Type = TypeTreatment.OTHER
			}
		);
	}
}
