using LNSF.Application.Interfaces;
using LNSF.Application.Validators;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using System.Net;

namespace LNSF.Application.Services;

public class TourService : ITourService
{
    private readonly ITourRepository _tourRepository;
    private readonly IPeopleRepository _peopleRepository;
    private readonly TourValidator _validator;

    public TourService(ITourRepository tourRepository,
        IPeopleRepository peoplesRepository,
        TourValidator validator)
    {
        _tourRepository = tourRepository;
        _peopleRepository = peoplesRepository;
        _validator = validator;
    }

    public async Task<List<Tour>> Query(TourFilter filter) => 
        await _tourRepository.Query(filter);

    public async Task<int> GetCount() => 
        await _tourRepository.GetCount();

    public async Task<Tour> CreateOpenTour(Tour tour)
    {
        var validationResult = _validator.Validate(tour);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);

        if (!await _peopleRepository.ExistsById(tour.PeopleId)) throw new AppException("Pessoa não encontrada!", HttpStatusCode.NotFound);
        if (await _tourRepository.PeopleHasOpenTour(tour.PeopleId)) throw new AppException("Pessoa possui passeio em aberto!", HttpStatusCode.Conflict);

        tour.Output = DateTime.Now;

        return await _tourRepository.Add(tour);
    }

    public async Task<Tour> UpdateOpenTourToClose(Tour newTour)
    {
        var validationResult = _validator.Validate(newTour);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);

        if (!await _tourRepository.ExistsByIdAndPeopleId(newTour.Id, newTour.PeopleId)) throw new AppException("Passeio não encontrado!", HttpStatusCode.NotFound);
        if (await _tourRepository.IsClosed(newTour.Id)) throw new AppException("Pessoa já retornou!", HttpStatusCode.Conflict);

        var oldTour = await _tourRepository.GetById(newTour.Id);
        oldTour.Input = DateTime.Now;
        oldTour.Note = newTour.Note;

        return await _tourRepository.Update(oldTour); 
    }

    public async Task<Tour> Update(Tour tour)
    {
        var validationResult = _validator.Validate(tour);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);
        
        if (!await _tourRepository.ExistsByIdAndPeopleId(tour.Id, tour.PeopleId)) throw new AppException("Passeio não encontrado!", HttpStatusCode.NotFound);

        return await _tourRepository.Update(tour);
    }
}
