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

    public async Task<Tour> Create(Tour tour)
    {
        var validationResult = _validator.Validate(tour);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);

        if (!await _peopleRepository.Exists(tour.PeopleId)) throw new AppException("Pessoa não encontrada!", HttpStatusCode.NotFound);
        if (await _tourRepository.PeopleHasOpenTour(tour.PeopleId)) throw new AppException("Pessoa possui passeio em aberto!", HttpStatusCode.Conflict);

        tour.Output = DateTime.Now;

        return await _tourRepository.Add(tour);
    }

    public async Task<Tour> Update(Tour tour)
    {
        var validationResult = _validator.Validate(tour);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);

        if (!await _tourRepository.ExistsByIdAndPeopleId(tour.Id, tour.PeopleId)) throw new AppException("Passeio não encontrado!", HttpStatusCode.NotFound);
        if (await _tourRepository.IsClosed(tour.Id)) throw new AppException("Pessoa já retornou!", HttpStatusCode.Conflict);

        tour.Input = DateTime.Now;

        return await _tourRepository.Update(tour); 
    }

    public async Task<Tour> UpdateAll(Tour tour)
    {
        var validationResult = _validator.Validate(tour);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);
        
        if (!await _tourRepository.ExistsByIdAndPeopleId(tour.Id, tour.PeopleId)) throw new AppException("Passeio não encontrado!", HttpStatusCode.NotFound);

        return await _tourRepository.Update(tour);
    }
}
