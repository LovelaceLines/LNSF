using LNSF.Domain.Repositories;
using LNSF.Domain.Filters;
using LNSF.Domain.Entities;
using LNSF.Domain.Enums;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace LNSF.Infra.Data.Repositories;

public class PeopleRepository : BaseRepository<People>, IPeopleRepository
{
    private readonly AppDbContext _context;
    private readonly IQueryable<Patient> _patients;
    private readonly IQueryable<Escort> _escorts;
    private readonly IQueryable<Hosting> _hostings;
    private readonly IQueryable<HostingEscort> _hostingsEscorts;

    public PeopleRepository(AppDbContext context) : base(context)
    {
        _context = context;
        _patients = _context.Patients.AsNoTracking();
        _escorts = _context.Escorts.AsNoTracking();
        _hostings = _context.Hostings.AsNoTracking();
        _hostingsEscorts = _context.HostingsEscorts.AsNoTracking();
    }

    public async Task<List<People>> Query(PeopleFilter filter)
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

        if (filter.Patient.HasValue) query = QueryPatient(query, filter.Patient.Value);
        if (filter.Escort.HasValue) query = QueryEscort(query, filter.Escort.Value);
        if (filter.Active.HasValue) query = QueryActive(query, filter.Active.Value);
        if (filter.Veteran.HasValue) query = QueryVeteran(query, filter.Veteran.Value);

        if (filter.OrderBy == OrderBy.Ascending) query = query.OrderBy(p => p.Name);
        else if (filter.OrderBy == OrderBy.Descending) query = query.OrderByDescending(p => p.Name);

        var peoples = await query
            .Skip(filter.Page.Page * filter.Page.PageSize)
            .Take(filter.Page.PageSize)
            .ToListAsync();

        return peoples;
    }

    protected static IQueryable<People> QueryGlobalFilter(IQueryable<People> peoples, string globalFilter) =>
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

    protected static IQueryable<People> QueryPeopleId(IQueryable<People> peoples, int id) =>
        peoples.Where(p => p.Id == id);

    protected static IQueryable<People> QueryName(IQueryable<People> peoples, string name) =>
        peoples.Where(p => p.Name.ToLower().Contains(name.ToLower()));

    protected static IQueryable<People> QueryRG(IQueryable<People> peoples, string rg) =>
        peoples.Where(p => p.RG.Contains(rg));

    protected static IQueryable<People> QueryIssuingBody(IQueryable<People> peoples, string issuingBody) =>
        peoples.Where(p => p.IssuingBody.ToLower().Contains(issuingBody.ToLower()));

    protected static IQueryable<People> QueryCPF(IQueryable<People> peoples, string cpf) =>
        peoples.Where(p => p.CPF.Contains(cpf));

    protected static IQueryable<People> QueryPhone(IQueryable<People> peoples, string phone) =>
        peoples.Where(p => p.Phone.Contains(phone));

    protected static IQueryable<People> QueryGender(IQueryable<People> peoples, Gender gender) =>
        peoples.Where(p => p.Gender == gender);

    protected static IQueryable<People> QueryBirthDate(IQueryable<People> peoples, DateTime birthDate) =>
        peoples.Where(p => p.BirthDate.Date == birthDate.Date);

    protected static IQueryable<People> QueryStreet(IQueryable<People> peoples, string street) =>
        peoples.Where(p => p.Street.ToLower().Contains(street.ToLower()));

    protected static IQueryable<People> QueryHouseNumber(IQueryable<People> peoples, string houseNumber) =>
        peoples.Where(p => p.HouseNumber.ToLower().Contains(houseNumber.ToLower()));

    protected static IQueryable<People> QueryNeighborhood(IQueryable<People> peoples, string neighborhood) =>
        peoples.Where(p => p.Neighborhood.ToLower().Contains(neighborhood.ToLower()));

    protected static IQueryable<People> QueryCity(IQueryable<People> peoples, string city) =>
        peoples.Where(p => p.City.ToLower().Contains(city.ToLower()));

    protected static IQueryable<People> QueryState(IQueryable<People> peoples, string state) =>
        peoples.Where(p => p.State.ToLower().Contains(state.ToLower()));

    protected static IQueryable<People> QueryNote(IQueryable<People> peoples, string note) =>
        peoples.Where(p => p.Note.ToLower().Contains(note.ToLower()));

    protected IQueryable<People> QueryPatient(IQueryable<People> peoples, bool getPatient) =>
        getPatient ? peoples.Where(p => _patients.Any(pt => pt.PeopleId == p.Id)) :
            peoples.Where(p => !_patients.Any(pt => pt.PeopleId == p.Id));

    protected IQueryable<People> QueryEscort(IQueryable<People> peoples, bool getEscort) =>
        getEscort ? peoples.Where(p => _escorts.Any(e => e.PeopleId == p.Id)) :
            peoples.Where(p => !_escorts.Any(e => e.PeopleId == p.Id));

    protected IQueryable<People> QueryActive(IQueryable<People> peoples, bool getActive) =>
        getActive ? peoples.Where(p =>
            _hostings.Any(h => h.Patient!.PeopleId == p.Id &&
                h.CheckIn <= DateTime.Now && DateTime.Now <= h.CheckOut)
            ||
            _hostingsEscorts.Any(he => he.Escort!.PeopleId == p.Id &&
                he.Hosting!.CheckIn <= DateTime.Now && DateTime.Now <= he.Hosting.CheckOut)) :
            peoples.Where(p =>
            !_hostings.Any(h => h.Patient!.PeopleId == p.Id &&
                h.CheckIn <= DateTime.Now && DateTime.Now <= h.CheckOut)
            &&
            !_hostingsEscorts.Any(he => he.Escort!.PeopleId == p.Id &&
                he.Hosting!.CheckIn <= DateTime.Now && DateTime.Now <= he.Hosting.CheckOut));

    protected IQueryable<People> QueryVeteran(IQueryable<People> peoples, bool getVeteran) =>
        getVeteran ? peoples.Where(p =>
            _hostings.Count(h => h.Patient!.PeopleId == p.Id) +
            _hostingsEscorts.Count(he => he.Escort!.PeopleId == p.Id) > 1) :
            peoples.Where(p =>
            _hostings.Count(h => h.Patient!.PeopleId == p.Id) +
            _hostingsEscorts.Count(he => he.Escort!.PeopleId == p.Id) <= 1);
}
