using LNSF.Domain.Entities;
using LNSF.Domain.Filters;

namespace LNSF.Application.Interfaces;

public interface IServiceRecordService
{
    Task<List<ServiceRecord>> Query(ServiceRecordFilter filter);
    Task<int> GetCount();
    Task<ServiceRecord> Create(ServiceRecord serviceRecord);
    Task<ServiceRecord> Update(ServiceRecord serviceRecord);
    Task<ServiceRecord> Delete(int id);
}
