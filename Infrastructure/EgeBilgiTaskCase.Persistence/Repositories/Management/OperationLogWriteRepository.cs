using EgeBilgiTaskCase.Application.Repositories.Management;
using EgeBilgiTaskCase.Domain.Entities.Management;
using EgeBilgiTaskCase.Persistence.Context;

namespace EgeBilgiTaskCase.Persistence.Repositories.Management
{
    public class OperationLogWriteRepository : WriteRepository<OperationLog>, IOperationLogWriteRepository
    {
        public OperationLogWriteRepository(EgeBilgiTaskCaseDbContext context) : base(context)
        {
        }
    }
}
