using LNSF.Domain.Repositories;
using LNSF.Domain.Entities;
using LNSF.Application.Validators;
using LNSF.Domain.DTOs;

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

    public async Task<ResultDTO<List<Tour>>> Get(Pagination pagination)
    {
        var validationResult = _paginationValidator.Validate(pagination);

        return validationResult.IsValid ?
            await _tourRepository.Get(pagination) :
            new ResultDTO<List<Tour>>(validationResult.ToString());
    }

    public async Task<ResultDTO<Tour>> Get(int id) =>
        await _tourRepository.Get(id);

    public async Task<ResultDTO<int>> GetQuantity() => 
        await _tourRepository.GetQuantity();

    public async Task<ResultDTO<Tour>> Post(Tour tour)
    {
        tour.Id = 0;
        tour.Input = null;

        var people = await _peopleRepository.Get(tour.PeopleId);

        if (people.Error == true) return new ResultDTO<Tour>("Pessoa não encontrda.");

        var validationResult = _tourPostValidator.Validate(tour);

        return validationResult.IsValid ? 
            await _tourRepository.Post(tour) :
            new ResultDTO<Tour>(validationResult.ToString()); 
    }

    public async Task<ResultDTO<Tour>> Put(Tour tour)
    {
        if (tour.Id == 0) return new ResultDTO<Tour>("Pesseio não encontrda.");

        var people = await _peopleRepository.Get(tour.PeopleId);
        
        if (people.Error == true) return new ResultDTO<Tour>("Pessoa não encontrda.");

        // tour.PeopleId = people.Data.Id;

        var validationResult = _tourPutValidator.Validate(tour);

        return validationResult.IsValid ?
            await _tourRepository.Put(tour) :
            new ResultDTO<Tour>(validationResult.ToString()); 
    }
}
