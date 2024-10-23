using LNSF.Application.Interfaces;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Repositories;
using System.Net;

namespace LNSF.Application.Services;

public class HostingEscortService(IHostingEscortRepository repository,
	IHostingRepository hostingRepository,
	IEscortRepository escortRepository) : IHostingEscortService
{
	public async Task<HostingEscort> Create(HostingEscort hostingEscort)
	{
		if (!await escortRepository.ExistsById(hostingEscort.EscortId)) throw new AppException("Acompanhante não encontrado", HttpStatusCode.NotFound);
		if (!await hostingRepository.ExistsById(hostingEscort.HostingId)) throw new AppException("Hospedagem não encontrada", HttpStatusCode.NotFound);
		if (await repository.ExistsByHostingIdAndEscortId(hostingEscort.HostingId, hostingEscort.EscortId)) throw new AppException("Acompanhante já está vinculado a esta hospedagem", HttpStatusCode.Conflict);
		if (await repository.ExistsWithDateConflict(hostingEscort.HostingId, hostingEscort.EscortId)) throw new AppException("Já existe uma hospedagem para este acompanhante neste período", HttpStatusCode.Conflict);

		return await repository.Add(hostingEscort);
	}

	public async Task<HostingEscort> Delete(HostingEscort hostingEscort)
	{
		if (!await repository.ExistsByHostingIdAndEscortId(hostingEscort.HostingId, hostingEscort.EscortId)) throw new AppException("Acompanhante não encontrado", HttpStatusCode.NotFound);

		hostingEscort = await repository.GetByHostingIdAndEscortId(hostingEscort.HostingId, hostingEscort.EscortId);

		return await repository.Remove(hostingEscort);
	}
}
