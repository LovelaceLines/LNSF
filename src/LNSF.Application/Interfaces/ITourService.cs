using LNSF.Domain.Entities;

namespace LNSF.Application.Interfaces;

public interface ITourService : IBaseService<Tour>
{
	Task<Tour> CreateOpenTour(Tour tour);
	Task<Tour> UpdateOpenTourToClose(Tour tour);
}
