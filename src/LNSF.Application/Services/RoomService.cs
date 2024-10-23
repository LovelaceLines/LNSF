using LNSF.Application.Interfaces;
using LNSF.Application.Validators;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Repositories;
using System.Net;

namespace LNSF.Application.Services;

public class RoomService(IRoomRepository repository,
	RoomValidator validator) : IRoomService
{
	public async Task<Room> Create(Room room)
	{
		var validationResult = validator.Validate(room);
		if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);

		if (await repository.ExistsByNumber(room.Number)) throw new AppException("Número do quarto já existe!", HttpStatusCode.Conflict);

		return await repository.Add(room);
	}

	public async Task<Room> Update(Room newRoom)
	{
		var validationResult = validator.Validate(newRoom);
		if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);

		var oldRoom = await repository.GetById(newRoom.Id);
		if (oldRoom.Number != newRoom.Number && await repository.ExistsByNumber(newRoom.Number)) throw new AppException("Número do quarto já existe!", HttpStatusCode.Conflict);

		return await repository.Update(newRoom);
	}
}
