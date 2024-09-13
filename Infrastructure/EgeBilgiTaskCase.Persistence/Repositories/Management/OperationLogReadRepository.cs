using EgeBilgiTaskCase.Application.Repositories.Management;
using EgeBilgiTaskCase.Domain.Entities.Management;
using EgeBilgiTaskCase.Persistence.Context;

namespace EgeBilgiTaskCase.Persistence.Repositories.Management
{
    public class OperationLogReadRepository : ReadRepository<OperationLog>, IOperationLogReadRepository
    {
        public OperationLogReadRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
