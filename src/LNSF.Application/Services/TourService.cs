using LNSF.Application.Interfaces;
using LNSF.Application.Validators;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Repositories;
using System.Net;

namespace LNSF.Application.Services;

public class TourService(ITourRepository repository,
	IPeopleRepository peopleRepository,
	TourValidator validator) : ITourService
{
	public async Task<Tour> CreateOpenTour(Tour tour)
	{
		var validationResult = validator.Validate(tour);
		if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);

		if (!await peopleRepository.ExistsById(tour.PeopleId)) throw new AppException("Pessoa não encontrada!", HttpStatusCode.NotFound);
		if (await repository.PeopleHasOpenTour(tour.PeopleId)) throw new AppException("Pessoa possui passeio em aberto!", HttpStatusCode.Conflict);

		tour.Output = DateTime.Now;
		tour.Input = null;

		return await repository.Add(tour);
	}

	public async Task<Tour> UpdateOpenTourToClose(Tour newTour)
	{
		var validationResult = validator.Validate(newTour);
		if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);

		if (!await repository.ExistsByIdAndPeopleId(newTour.Id, newTour.PeopleId)) throw new AppException("Passeio não encontrado!", HttpStatusCode.NotFound);
		if (await repository.IsClosed(newTour.Id)) throw new AppException("Pessoa já retornou!", HttpStatusCode.Conflict);

		var oldTour = await repository.GetById(newTour.Id);
		oldTour.Input = DateTime.Now;
		oldTour.Note = newTour.Note;

		return await repository.Update(oldTour);
	}

	public async Task<Tour> Update(Tour tour)
	{
		var validationResult = validator.Validate(tour);
		if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);

		if (!await repository.ExistsByIdAndPeopleId(tour.Id, tour.PeopleId)) throw new AppException("Passeio não encontrado!", HttpStatusCode.NotFound);

		return await repository.Update(tour);
	}
}
