using LNSF.Domain.Repositories;
using LNSF.Domain.Entities;
using LNSF.Application.Validators;
using LNSF.Domain.DTOs;
using LNSF.Domain.Exceptions;

namespace LNSF.Application.Services;

public class TourService
{
    private readonly IToursRepository _tourRepository;
    private readonly IPeoplesRepository _peopleRepository;
    private readonly TourPostValidator _tourPostValidator;
    private readonly TourPutValidator _tourPutValidator;
    private readonly PaginationValidator _paginationValidator;

    public TourService(IToursRepository tourRepository,
        IPeoplesRepository peoplesRepository,
        TourPostValidator tourPostValidator,
        TourPutValidator tourPutValidator,
        PaginationValidator paginationValidator)
    {
        _tourRepository = tourRepository;
        _peopleRepository = peoplesRepository;
        _tourPostValidator = tourPostValidator;
        _tourPutValidator = tourPutValidator;
        _paginationValidator = paginationValidator;
    }

    public async Task<List<Tour>> Get(Pagination pagination)
    {
        var validationResult = _paginationValidator.Validate(pagination);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString());

        return await _tourRepository.Get(pagination);
    }

    public async Task<Tour> Get(int id) =>
        await _tourRepository.Get(id);

    public async Task<int> GetQuantity() => 
        await _tourRepository.GetQuantity();

    public async Task<Tour> Post(Tour tour)
    {
        var validationResult = _tourPostValidator.Validate(tour);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString());

        await _peopleRepository.Get(tour.PeopleId);

        tour.Id = 0;
        tour.Input = null;

        return await _tourRepository.Post(tour);
    }

    public async Task<Tour> Put(Tour tour)
    {
        var validationResult = _tourPutValidator.Validate(tour);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString());

        var old_tour = await _tourRepository.Get(tour.Id);
        
        tour.Output = old_tour.Output;
        tour.PeopleId = old_tour.PeopleId;
        
        return await _tourRepository.Put(tour); 
    }
}
