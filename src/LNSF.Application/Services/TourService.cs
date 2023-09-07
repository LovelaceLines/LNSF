using LNSF.Domain.Repositories;
using LNSF.Domain.Entities;
using LNSF.Domain.Views;
using LNSF.Application.Validators;

namespace LNSF.Application.Services;

public class TourService
{
    private readonly ITourRepository _tourRepository;
    private readonly TourOutputValidator _outputValidator;
    private readonly TourInputValidator _inputValidator;

    public TourService(ITourRepository tourRepository,
        TourOutputValidator outputValidator,
        TourInputValidator inputValidator)
    {
        _tourRepository = tourRepository;
        _outputValidator = outputValidator;
        _inputValidator = inputValidator;
    }

    public async Task<List<Tour>> Get()
    {
        return await _tourRepository.Get();
    }

    public async Task<Tour> Get(int id)
    {
        return await _tourRepository.Get(id);
    }

    public async Task<Tour> AddOutput(ITourOutput output)
    {
        var validationResult = _outputValidator.Validate(output);

        if (!validationResult.IsValid)
        {
            return new Tour();
        }

        return await _tourRepository.AddOutput(output);
    }

    public async Task<Tour> AddInput(ITourInput input)
    {
        var validationResult = _inputValidator.Validate(input);

        if (!validationResult.IsValid)
        {
            return new Tour();
        }

        return await _tourRepository.AddInput(input);
    }
}
