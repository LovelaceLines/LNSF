using LNSF.Domain.Repositories;
using LNSF.Domain.Entities;
using LNSF.Domain.Views;
using LNSF.Application.Validators;
using LNSF.Domain.DTOs;

namespace LNSF.Application.Services;

public class TourService
{
    private readonly ITourRepository _tourRepository;
    private readonly TourOutputValidator _outputValidator;
    private readonly TourInputValidator _inputValidator;
    private readonly PaginationValidator _paginationValidator;

    public TourService(ITourRepository tourRepository,
        TourOutputValidator outputValidator,
        TourInputValidator inputValidator,
        PaginationValidator paginationValidator)
    {
        _tourRepository = tourRepository;
        _outputValidator = outputValidator;
        _inputValidator = inputValidator;
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

    public async Task<ResultDTO<Tour>> PostOutput(Tour output)
    {
        var validationResult = _outputValidator.Validate(output);

        if (!validationResult.IsValid) return new ResultDTO<Tour>(validationResult.ToString());
            
        output.Id = null;

        return await _tourRepository.PostOutput(output);
    }

    public async Task<ResultDTO<Tour>> PutInput(Tour input)
    {
        var validationResult = _inputValidator.Validate(input);

        return validationResult.IsValid ?
            await _tourRepository.PutInput(input) :
            new ResultDTO<Tour>(validationResult.ToString()); 
    }
}
