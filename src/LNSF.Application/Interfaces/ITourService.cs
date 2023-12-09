using LNSF.Domain.Entities;
using LNSF.Domain.Filters;

namespace LNSF.Application.Interfaces;

public interface ITourService
{
    Task<List<Tour>> Query(TourFilter filter); 
    Task<int> GetCount();
    Task<Tour> Create(Tour tour);
    Task<Tour> Update(Tour tour);
    Task<Tour> UpdateAll(Tour tour);
}
