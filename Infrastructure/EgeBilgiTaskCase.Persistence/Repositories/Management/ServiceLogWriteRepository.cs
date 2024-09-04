using EgeBilgiTaskCase.Application.Repositories.Management;
using EgeBilgiTaskCase.Domain.Entities.Management;
using EgeBilgiTaskCase.Persistence.Context;

namespace EgeBilgiTaskCase.Persistence.Repositories.Management
{
    public class ServiceLogWriteRepository : WriteRepository<ServiceLog>, IServiceLogWriteRepository
    {
        public ServiceLogWriteRepository(EgeBilgiTaskCaseDbContext context) : base(context)
        {
        }
    }
}
