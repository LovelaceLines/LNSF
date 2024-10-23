using LNSF.Domain.Entities;

namespace LNSF.Application.Interfaces;

public interface IHostingEscortService : IBaseService<HostingEscort>
{
	Task<HostingEscort> Delete(HostingEscort hostingEscort);
}
