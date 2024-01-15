using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;

namespace LNSF.Application.Interfaces;

public interface IHostingService
{
    Task<List<HostingDTO>> Query(HostingFilter filter);
    Task<int> GetCount();
    Task<Hosting> Create(Hosting hosting);
    Task<Hosting> Update(Hosting hosting);
}