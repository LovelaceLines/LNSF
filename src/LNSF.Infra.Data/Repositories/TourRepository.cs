using System.Linq.Expressions;
using LNSF.Domain.Entities;
using LNSF.Domain.Enums;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace LNSF.Infra.Data.Repositories;

public class ToursRepository : BaseRepository<Tour>, ITourRepository
{
    private readonly AppDbContext _context;
    private readonly IQueryable<Tour> _tours;
    private readonly IQueryable<People> _peoples;

    public ToursRepository(AppDbContext context) : base(context)
    {
        _context = context;
        _tours = _context.Tours.AsNoTracking();
        _peoples = _context.Peoples.AsNoTracking();
    }

    public async Task<List<Tour>> Query(TourFilter filter)
    {
        var query = _tours;

        if (!filter.GlobalFilter.IsNullOrEmpty()) query = QueryGlobalFilter(query, filter.GlobalFilter!, _peoples);

        if (filter.Id.HasValue) query = QueryTourId(query, filter.Id.Value);
        if (filter.Output.HasValue) query = QueryOutput(query, filter.Output.Value);
        if (filter.Input.HasValue) query = QueryInput(query, filter.Input.Value);
        if (!filter.Note.IsNullOrEmpty()) query = QueryNote(query, filter.Note!);
        if (filter.PeopleId.HasValue) query = QueryPeopleId(query, filter.PeopleId.Value);

        if (filter.InOpen.HasValue) query = QueryInOpen(query, filter.InOpen.Value);

        if (filter.OrderBy == OrderBy.Ascending) query = query.OrderBy(t => t.Output);
        else if (filter.OrderBy == OrderBy.Descending) query = query.OrderByDescending(t => t.Output);

        var tours = await query
            .Skip(filter.Page.Page * filter.Page.PageSize)
            .Take(filter.Page.PageSize)
            .Select(Build(filter.GetPeople ?? false))
            .ToListAsync();

        return tours;
    }

    private static Expression<Func<Tour, Tour>> Build(bool getPeople) =>
        t => new Tour()
        {
            Id = t.Id,
            Output = t.Output,
            Input = t.Input,
            Note = t.Note,
            PeopleId = t.PeopleId,
            People = getPeople != true ? null : t.People
        };

    public static IQueryable<Tour> QueryGlobalFilter(IQueryable<Tour> query, string globalFilter, IQueryable<People> peoples) =>
        query.Where(t =>
            QueryNote(query, globalFilter).Any(q => q.Id == t.Id) ||
            PeopleRepository.QueryGlobalFilter(peoples, globalFilter).Any(p => p.Id == t.PeopleId));

    public static IQueryable<Tour> QueryTourId(IQueryable<Tour> query, int id) =>
        query.Where(t => t.Id == id);

    public static IQueryable<Tour> QueryOutput(IQueryable<Tour> query, DateTime output) =>
        query.Where(t => t.Output >= output);

    public static IQueryable<Tour> QueryInput(IQueryable<Tour> query, DateTime input) =>
        query.Where(t => t.Input <= input);

    public static IQueryable<Tour> QueryInOpen(IQueryable<Tour> query, bool inOpen) =>
        inOpen ? query.Where(t => t.Input == null) :
            query.Where(t => t.Input != null);

    public static IQueryable<Tour> QueryNote(IQueryable<Tour> query, string note) =>
        query.Where(t => t.Note.ToLower().Contains(note.ToLower()));

    public static IQueryable<Tour> QueryPeopleId(IQueryable<Tour> query, int peopleId) =>
        query.Where(t => t.PeopleId == peopleId);

    public async Task<bool> IsClosed(int id) =>
        await _tours.AnyAsync(t => t.Id == id && t.Input != null);

    public async Task<bool> IsOpen(int id) =>
        await _tours.AnyAsync(t => t.Id == id && t.Input == null);

    public async Task<bool> PeopleHasOpenTour(int peopleId) =>
        await _context.Tours.AsNoTracking()
            .Where(x => x.PeopleId == peopleId && x.Input == null)
            .AnyAsync();

    public async Task<bool> ExistsByIdAndPeopleId(int id, int peopleId) =>
        await _tours.AnyAsync(t => t.Id == id && t.PeopleId == peopleId);
}
