using LNSF.Domain.Entities;
using LNSF.Domain.Filters;

namespace LNSF.Application.Interfaces;

public interface ITourService
{
    public Task<List<Tour>> Query(TourFilter filter); 
    public Task<int> GetCount();
    public Task<Tour> Create(Tour tour);
    public Task<Tour> Update(Tour tour);
    public Task<Tour> UpdateAll(Tour tour);
}
