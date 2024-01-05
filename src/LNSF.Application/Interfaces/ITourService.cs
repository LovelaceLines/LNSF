using LNSF.Domain.Entities;
using LNSF.Domain.Filters;

namespace LNSF.Application.Interfaces;

public interface ITourService
{
    Task<List<Tour>> Query(TourFilter filter); 
    Task<int> GetCount();
    Task<Tour> CreateOpenTour(Tour tour);
    Task<Tour> UpdateOpenTourToClose(Tour tour);
    Task<Tour> Update(Tour tour);
}
