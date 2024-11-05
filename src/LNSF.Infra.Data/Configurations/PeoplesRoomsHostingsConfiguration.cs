using LNSF.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LNSF.Infra.Data.Configurations;

public class PeoplesRoomsHostingsConfiguration : BaseConfiguration<PeopleRoomHosting>
{
	public override void Configure(EntityTypeBuilder<PeopleRoomHosting> builder)
	{
		base.Configure(builder);

		builder.HasKey(pr => pr.Id);

		builder.HasIndex(pr => new { pr.RoomId, pr.PeopleId, pr.HostingId });

		builder.HasOne(pr => pr.People)
			.WithMany()
			.HasForeignKey(pr => pr.PeopleId);

		builder.HasOne(pr => pr.Room)
			.WithMany()
			.HasForeignKey(pr => pr.RoomId);

		builder.HasOne(pr => pr.Hosting)
			.WithMany()
			.HasForeignKey(pr => pr.HostingId);
	}
}
