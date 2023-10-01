using LNSF.Domain.Repositories;
using LNSF.Domain.Entities;
using LNSF.Application.Validators;
using LNSF.Domain.DTOs;
using LNSF.Domain.Exceptions;
using System.Linq;

namespace LNSF.Application.Services;

public class TourService
{
    private readonly IToursRepository _tourRepository;
    private readonly IPeoplesRepository _peopleRepository;
    private readonly TourFiltersValidator _tourFilters;
    private readonly TourPostValidator _tourPostValidator;
    private readonly TourPutValidator _tourPutValidator;

    public TourService(IToursRepository tourRepository,
        IPeoplesRepository peoplesRepository,
        TourFiltersValidator tourFilters,
        TourPostValidator tourPostValidator,
        TourPutValidator tourPutValidator)
    {
        _tourRepository = tourRepository;
        _peopleRepository = peoplesRepository;
        _tourFilters = tourFilters;
        _tourPostValidator = tourPostValidator;
        _tourPutValidator = tourPutValidator;
    }

    public async Task<List<Tour>> Query(TourFilters filters)
    {
        var validationResult = _tourFilters.Validate(filters);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString());

        return await _tourRepository.Query(filters);
    }

    public async Task<Tour> Get(int id) =>
        await _tourRepository.Get(id);

    public async Task<int> GetQuantity() => 
        await _tourRepository.GetQuantity();

    public async Task<Tour> Create(Tour tour)
    {
        var validationResult = _tourPostValidator.Validate(tour);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString());

        await _peopleRepository.Get(tour.PeopleId);

        var query = await _tourRepository.Query(new TourFilters { PeopleId = tour.PeopleId, Input = null });
        if (query.Count > 0) throw new AppException("Pessoa possui pesseio em aberto!");

        tour.Id = 0;
        tour.Output = DateTime.Now;

        return await _tourRepository.Post(tour);
    }

    public async Task<Tour> Update(Tour tour)
    {
        var validationResult = _tourPutValidator.Validate(tour);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString());

        await _tourRepository.Get(tour.Id);
        await _peopleRepository.Get(tour.PeopleId);

        var query = await _tourRepository.Query(new TourFilters { PeopleId = tour.PeopleId, Input = null });
        if (query.Count != 1) throw new AppException("Pessoa não possui pesseio em aberto!");

        tour.Input = DateTime.Now;

        return await _tourRepository.Put(tour); 
    }

    public async Task<Tour> UpdateAll(Tour tour)
    {
        var validationResult = _tourPutValidator.Validate(tour);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString());

        var query = await Query(new TourFilters { Id = tour.Id, PeopleId = tour.PeopleId });
        if (query.Count != 1) throw new AppException("Id e PeopleId não existem!");

        return await _tourRepository.Put(tour);
    }
}
