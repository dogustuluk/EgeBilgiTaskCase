using EgeBilgiTaskCase.Application.Repositories.Management;
using EgeBilgiTaskCase.Domain.Entities.Management;
using EgeBilgiTaskCase.Persistence.Context;

namespace EgeBilgiTaskCase.Persistence.Repositories.Management
{
    public class ServiceLogReadRepository : ReadRepository<ServiceLog>, IServiceLogReadRepository
    {
        public ServiceLogReadRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
