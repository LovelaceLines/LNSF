using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using LNSF.Test.DTOs;
using LNSF.Test.Fakers;
using LNSF.Test.Global;
using System.Net;

namespace LNSF.Test.API;

public class RoomTestApi : GlobalClientRequest
{
	[Fact]
	public async Task QueryValidRoomId_Ok()
	{
		var room = await GetRoom();

		var result = await GetFromQuery<QueryResult<Room>>(_roomClient, new RoomFilter
		{
			Id = room.Id,
			Number = room.Number,
			Bathroom = room.Bathroom,
			Beds = room.Beds,
			Storey = room.Storey,
			Available = room.Available
		});
		var RoomQueried = result.Items.Single();

		Assert.Equivalent(room, RoomQueried);
	}

	[Fact]
	public async Task Post_ValidRoom_Ok()
	{
		var room = new RoomFake().Generate();

		var roomPosted = await PostFromBody<Room>(_roomClient, room);

		Assert.Equal(room.Number, roomPosted.Number);
		Assert.Equal(room.Bathroom, roomPosted.Bathroom);
		Assert.Equal(room.Beds, roomPosted.Beds);
		Assert.Equal(room.Storey, roomPosted.Storey);
		Assert.Equal(room.Available, roomPosted.Available);
	}

	[Fact]
	public async Task Post_InvalidRoom_BadResquest()
	{
		var roomWithouNumber = new RoomFake(number: "").Generate();
		var roomWithouBads = new RoomFake(beds: 0).Generate();
		var roomWithNegativeStorey = new RoomFake(storey: new Random().Next(-int.MaxValue, -1)).Generate();

		var exceptionWithoutNumber = await PostFromBody<AppHttpResponse>(_roomClient, roomWithouNumber);
		var exceptionWithoutBads = await PostFromBody<AppHttpResponse>(_roomClient, roomWithouBads);
		var exceptionWithNegativeStorey = await PostFromBody<AppHttpResponse>(_roomClient, roomWithNegativeStorey);

		Assert.Equal(HttpStatusCode.BadRequest, exceptionWithoutNumber.StatusCode);
		Assert.Equal(HttpStatusCode.BadRequest, exceptionWithoutBads.StatusCode);
		Assert.Equal(HttpStatusCode.BadRequest, exceptionWithNegativeStorey.StatusCode);
	}

	[Fact]
	public async Task Put_RoomValid_Ok()
	{
		var room = await GetRoom();
		var roomToPut = new RoomFake(id: room.Id).Generate();

		var roomPuted = await PutFromBody<Room>(_roomClient, roomToPut);

		Assert.Equal(roomToPut.Id, roomPuted.Id);
		Assert.Equal(roomToPut.Number, roomPuted.Number);
		Assert.Equal(roomToPut.Bathroom, roomPuted.Bathroom);
		Assert.Equal(roomToPut.Beds, roomPuted.Beds);
		Assert.Equal(roomToPut.Storey, roomPuted.Storey);
		Assert.Equal(roomToPut.Available, roomPuted.Available);
	}

	[Fact]
	public async Task Put_ValidRoomWithSameRoomNumber_Ok()
	{
		var room = await GetRoom();
		var roomToPut = new RoomFake(id: room.Id, number: room.Number).Generate();

		var roomPuted = await PutFromBody<Room>(_roomClient, roomToPut);

		Assert.Equal(roomToPut.Id, roomPuted.Id);
		Assert.Equal(roomToPut.Number, roomPuted.Number);
		Assert.Equal(roomToPut.Bathroom, roomPuted.Bathroom);
		Assert.Equal(roomToPut.Beds, roomPuted.Beds);
		Assert.Equal(roomToPut.Storey, roomPuted.Storey);
		Assert.Equal(roomToPut.Available, roomPuted.Available);
	}

	[Fact]
	public async Task Put_RoomInvalidWithEmptyNumber_BadRequest()
	{
		var room = await GetRoom();

		var roomToPutWithoutNumber = new RoomFake(id: room.Id, number: "").Generate();
		var roomToPutWithoutBads = new RoomFake(id: room.Id, beds: 0).Generate();
		var roomToPutWithNegativeStorey = new RoomFake(id: room.Id, storey: new Random().Next(-int.MaxValue, -1)).Generate();

		var exceptionWithoutNumber = await PutFromBody<AppHttpResponse>(_roomClient, roomToPutWithoutNumber);
		var exceptionWithoutBads = await PutFromBody<AppHttpResponse>(_roomClient, roomToPutWithoutBads);
		var exceptionWithNegativeStorey = await PutFromBody<AppHttpResponse>(_roomClient, roomToPutWithNegativeStorey);

		Assert.Equal(HttpStatusCode.BadRequest, exceptionWithoutNumber.StatusCode);
		Assert.Equal(HttpStatusCode.BadRequest, exceptionWithoutBads.StatusCode);
		Assert.Equal(HttpStatusCode.BadRequest, exceptionWithNegativeStorey.StatusCode);
	}
}
