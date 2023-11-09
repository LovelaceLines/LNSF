using LNSF.Domain.Repositories;
using LNSF.Domain.Entities;
using LNSF.Application.Validators;
using LNSF.Domain.Filters;
using LNSF.Domain.Exceptions;
using System.Net;
using LNSF.Application.Interfaces;

namespace LNSF.Application.Services;

public class TourService : ITourService
{
    private readonly IToursRepository _tourRepository;
    private readonly IPeopleRepository _peopleRepository;
    private readonly TourValidator _validator;

    public TourService(IToursRepository tourRepository,
        IPeopleRepository peoplesRepository,
        TourValidator validator)
    {
        _tourRepository = tourRepository;
        _peopleRepository = peoplesRepository;
        _validator = validator;
    }

    public async Task<List<Tour>> Query(TourFilter filter) => 
        await _tourRepository.Query(filter);

    public async Task<Tour> Get(int id) =>
        await _tourRepository.Get(id);

    public async Task<int> GetCount() => 
        await _tourRepository.GetCount();

    public async Task<Tour> Create(Tour tour)
    {
        var validationResult = _validator.Validate(tour);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);

        if (!await _peopleRepository.Exists(tour.PeopleId)) throw new AppException("Pessoa não encontrada!", HttpStatusCode.UnprocessableEntity);
        if (await _tourRepository.PeopleHasOpenTour(tour.PeopleId)) throw new AppException("Pessoa possui pesseio em aberto!", HttpStatusCode.UnprocessableEntity);

        tour.Output = DateTime.Now;

        return await _tourRepository.Add(tour);
    }

    public async Task<Tour> Update(Tour tour)
    {
        var validationResult = _validator.Validate(tour);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);
        
        if (!await _peopleRepository.Exists(tour.PeopleId)) throw new AppException("Pessoa não encontrada!", HttpStatusCode.UnprocessableEntity);
        if (!await _tourRepository.Exists(tour.Id)) throw new AppException("Passeio não encontrado!", HttpStatusCode.UnprocessableEntity);
        if (!await _tourRepository.IsClosed(tour.Id)) throw new AppException("Pessoa não saiu!", HttpStatusCode.UnprocessableEntity);

        tour.Input = DateTime.Now;

        return await _tourRepository.Update(tour); 
    }

    public async Task<Tour> UpdateAll(Tour tour)
    {
        var validationResult = _validator.Validate(tour);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);
        
        if (!await _tourRepository.Exists(tour.Id)) throw new AppException("Passeio não encontrado!", HttpStatusCode.UnprocessableEntity);
        if (tour.Output > tour.Input) throw new AppException("Data de saída maior que data de entrada!", HttpStatusCode.UnprocessableEntity);

        return await _tourRepository.Update(tour);
    }
}
