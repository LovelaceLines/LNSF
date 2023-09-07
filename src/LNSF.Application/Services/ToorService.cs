using LNSF.Domain.Repositories;
using LNSF.Domain.Entities;
using LNSF.Domain.Views;
using LNSF.Application.Validators;

namespace LNSF.Application.Services;

public class ToorService
{
    private readonly IToorRepository _toorRepository;
    private readonly ToorOutputValidator _outputValidator;
    private readonly ToorInputValidator _inputValidator;

    public ToorService(IToorRepository toorRepository,
        ToorOutputValidator outputValidator,
        ToorInputValidator inputValidator)
    {
        _toorRepository = toorRepository;
        _outputValidator = outputValidator;
        _inputValidator = inputValidator;
    }

    public async Task<List<Toor>> Get()
    {
        return await _toorRepository.Get();
    }

    public async Task<Toor> Get(int id)
    {
        return await _toorRepository.Get(id);
    }

    public async Task<Toor> AddOutput(IToorOutput output)
    {
        var validationResult = _outputValidator.Validate(output);

        if (!validationResult.IsValid)
        {
            return new Toor();
        }

        return await _toorRepository.AddOutput(output);
    }

    public async Task<Toor> AddInput(IToorInput input)
    {
        var validationResult = _inputValidator.Validate(input);

        if (!validationResult.IsValid)
        {
            return new Toor();
        }

        return await _toorRepository.AddInput(input);
    }
}
