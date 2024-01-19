using LNSF.Domain.Repositories;
using LNSF.Domain.Filters;
using LNSF.Domain.Entities;
using LNSF.Domain.Enums;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using LNSF.Domain.DTOs;

namespace LNSF.Infra.Data.Repositories;

public class PeopleRepository : BaseRepository<People>, IPeopleRepository
{
    private readonly AppDbContext _context;
    private readonly IQueryable<People> _peoples;
    private readonly IQueryable<Patient> _patients;
    private readonly IQueryable<Escort> _escorts;
    private readonly IQueryable<Hosting> _hostings;
    private readonly IQueryable<HostingEscort> _hostingsEscorts;

    public PeopleRepository(AppDbContext context) : base(context)
    {
        _context = context;
        _peoples = _context.Peoples.AsNoTracking();
        _patients = _context.Patients.AsNoTracking();
        _escorts = _context.Escorts.AsNoTracking();
        _hostings = _context.Hostings.AsNoTracking();
        _hostingsEscorts = _context.HostingsEscorts.AsNoTracking();
    }

    public async Task<List<PeopleDTO>> Query(PeopleFilter filter)
    {
        var query = _context.Peoples.AsNoTracking();

        if (!filter.GlobalFilter.IsNullOrEmpty()) query = QueryGlobalFilter(query, filter.GlobalFilter!);

        if (filter.Id.HasValue) query = QueryPeopleId(query, filter.Id.Value);
        if (!filter.Name.IsNullOrEmpty()) query = QueryName(query, filter.Name!);
        if (!filter.RG.IsNullOrEmpty()) query = QueryRG(query, filter.RG!);
        if (!filter.CPF.IsNullOrEmpty()) query = QueryCPF(query, filter.CPF!);
        if (!filter.Phone.IsNullOrEmpty()) query = QueryPhone(query, filter.Phone!);
        if (filter.Gender.HasValue) query = QueryGender(query, filter.Gender.Value);
        if (filter.BirthDate.HasValue) query = QueryBirthDate(query, filter.BirthDate.Value);
        if (!filter.Street.IsNullOrEmpty()) query = QueryStreet(query, filter.Street!);
        if (!filter.HouseNumber.IsNullOrEmpty()) query = QueryHouseNumber(query, filter.HouseNumber!);
        if (!filter.Neighborhood.IsNullOrEmpty()) query = QueryNeighborhood(query, filter.Neighborhood!);
        if (!filter.City.IsNullOrEmpty()) query = QueryCity(query, filter.City!);
        if (!filter.State.IsNullOrEmpty()) query = QueryState(query, filter.State!);
        if (!filter.Note.IsNullOrEmpty()) query = QueryNote(query, filter.Note!);

        if (filter.Patient.HasValue) query = QueryPatient(query, filter.Patient.Value, _patients);
        if (filter.Escort.HasValue) query = QueryEscort(query, filter.Escort.Value, _escorts);
        if (filter.Active.HasValue) query = QueryActive(query, filter.Active.Value, _patients, _escorts, _hostings, _hostingsEscorts);
        if (filter.Veteran.HasValue) query = QueryVeteran(query, filter.Veteran.Value, _hostings, _hostingsEscorts);

        if (filter.OrderBy == OrderBy.Ascending) query = query.OrderBy(p => p.Name);
        else if (filter.OrderBy == OrderBy.Descending) query = query.OrderByDescending(p => p.Name);

        var peoples = await query
            .Skip(filter.Page.Page * filter.Page.PageSize)
            .Take(filter.Page.PageSize)
            .ToListAsync();

        var peoplesDTO = ConvertToDTO(peoples);

        return peoplesDTO;
    }

    public static IQueryable<People> QueryGlobalFilter(IQueryable<People> peoples, string globalFilter) =>
        peoples.Where(p =>
            p.Name.ToLower().Contains(globalFilter.ToLower()) ||
            p.RG.Contains(globalFilter) ||
            p.IssuingBody.ToLower().Contains(globalFilter.ToLower()) ||
            p.CPF.Contains(globalFilter) ||
            p.Phone.Contains(globalFilter) ||
            p.Street.ToLower().Contains(globalFilter.ToLower()) ||
            p.HouseNumber.ToLower().Contains(globalFilter.ToLower()) ||
            p.Neighborhood.ToLower().Contains(globalFilter.ToLower()) ||
            p.City.ToLower().Contains(globalFilter.ToLower()) ||
            p.State.ToLower().Contains(globalFilter.ToLower()) ||
            p.Note.ToLower().Contains(globalFilter.ToLower()));

    public static IQueryable<People> QueryPeopleId(IQueryable<People> peoples, int id) =>
        peoples.Where(p => p.Id == id);

    public static IQueryable<People> QueryName(IQueryable<People> peoples, string name) =>
        peoples.Where(p => p.Name.ToLower().Contains(name.ToLower()));

    public static IQueryable<People> QueryRG(IQueryable<People> peoples, string rg) =>
        peoples.Where(p => p.RG.Contains(rg));

    public static IQueryable<People> QueryIssuingBody(IQueryable<People> peoples, string issuingBody) =>
        peoples.Where(p => p.IssuingBody.ToLower().Contains(issuingBody.ToLower()));

    public static IQueryable<People> QueryCPF(IQueryable<People> peoples, string cpf) =>
        peoples.Where(p => p.CPF.Contains(cpf));

    public static IQueryable<People> QueryPhone(IQueryable<People> peoples, string phone) =>
        peoples.Where(p => p.Phone.Contains(phone));

    public static IQueryable<People> QueryGender(IQueryable<People> peoples, Gender gender) =>
        peoples.Where(p => p.Gender == gender);

    public static IQueryable<People> QueryBirthDate(IQueryable<People> peoples, DateTime birthDate) =>
        peoples.Where(p => p.BirthDate.Date == birthDate.Date);

    public static IQueryable<People> QueryStreet(IQueryable<People> peoples, string street) =>
        peoples.Where(p => p.Street.ToLower().Contains(street.ToLower()));

    public static IQueryable<People> QueryHouseNumber(IQueryable<People> peoples, string houseNumber) =>
        peoples.Where(p => p.HouseNumber.ToLower().Contains(houseNumber.ToLower()));

    public static IQueryable<People> QueryNeighborhood(IQueryable<People> peoples, string neighborhood) =>
        peoples.Where(p => p.Neighborhood.ToLower().Contains(neighborhood.ToLower()));

    public static IQueryable<People> QueryCity(IQueryable<People> peoples, string city) =>
        peoples.Where(p => p.City.ToLower().Contains(city.ToLower()));

    public static IQueryable<People> QueryState(IQueryable<People> peoples, string state) =>
        peoples.Where(p => p.State.ToLower().Contains(state.ToLower()));

    public static IQueryable<People> QueryNote(IQueryable<People> peoples, string note) =>
        peoples.Where(p => p.Note.ToLower().Contains(note.ToLower()));

    public static IQueryable<People> QueryPatient(IQueryable<People> peoples, bool getPatient, IQueryable<Patient> patients) =>
        getPatient ? peoples.Where(p => patients.Any(pt => pt.PeopleId == p.Id)) :
            peoples.Where(p => !patients.Any(pt => pt.PeopleId == p.Id));

    public static IQueryable<People> QueryEscort(IQueryable<People> peoples, bool getEscort, IQueryable<Escort> escorts) =>
        getEscort ? peoples.Where(p => escorts.Any(e => e.PeopleId == p.Id)) :
            peoples.Where(p => !escorts.Any(e => e.PeopleId == p.Id));

    public static IQueryable<People> QueryActive(IQueryable<People> peoples, bool getActive, IQueryable<Patient> patients, IQueryable<Escort> escorts, IQueryable<Hosting> hostings, IQueryable<HostingEscort> hostingsEscorts) =>
        getActive ? peoples.Where(p => PatientRepository.QueryActive(patients, hostings, true).Any(pt => pt.PeopleId == p.Id) ||
            EscortRepository.QueryActive(escorts, getActive, hostings, hostingsEscorts).Any(e => e.PeopleId == p.Id)) :
            peoples.Where(p => !PatientRepository.QueryActive(patients, hostings, true).Any(pt => pt.PeopleId == p.Id) &&
                !EscortRepository.QueryActive(escorts, getActive, hostings, hostingsEscorts).Any(e => e.PeopleId == p.Id));

    public static IQueryable<People> QueryVeteran(IQueryable<People> peoples, bool getVeteran, IQueryable<Hosting> hostings, IQueryable<HostingEscort> hostingsEscorts) =>
        getVeteran ? peoples.Where(p => hostings.Count(h => h.Patient!.PeopleId == p.Id) + hostingsEscorts.Count(he => he.Escort!.PeopleId == p.Id) > 1) :
            peoples.Where(p => hostings.Count(h => h.Patient!.PeopleId == p.Id) + hostingsEscorts.Count(he => he.Escort!.PeopleId == p.Id) <= 1);

    public bool IsVeteran(int peopleId)
    {
        var patientCount = _hostings.Count(h => h.Patient!.PeopleId == peopleId);
        var escortCount = _hostingsEscorts.Count(he => he.Escort!.PeopleId == peopleId);
        return patientCount + escortCount > 1;
    }

    public bool IsPatient(int peopleId) =>
        _patients.Any(p => p.PeopleId == peopleId);

    public bool IsEscort(int peopleId) =>
        _escorts.Any(e => e.PeopleId == peopleId);

    public List<PeopleDTO> ConvertToDTO(List<People> peoples)
    {
        var peoplesDTO = new List<PeopleDTO>();

        peoples.ForEach(people =>
        {
            var peopleDTO = new PeopleDTO(people);
            peopleDTO.Experience = IsVeteran(people.Id) ? "Veterano" : "Novato";
            peopleDTO.Status = IsPatient(people.Id) ? "Paciente" : IsEscort(people.Id) ? "Acompanhante" : "Sem status";
            peoplesDTO.Add(peopleDTO);
        });

        return peoplesDTO;
    }
}
