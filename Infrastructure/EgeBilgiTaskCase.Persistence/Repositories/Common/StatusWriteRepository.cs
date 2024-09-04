using EgeBilgiTaskCase.Application.Repositories.Common;
using EgeBilgiTaskCase.Domain.Entities.Common;
using EgeBilgiTaskCase.Persistence.Context;

namespace EgeBilgiTaskCase.Persistence.Repositories.Common
{
    public class StatusWriteRepository : WriteRepository<Status>, IStatusWriteRepository
    {
        public StatusWriteRepository(EgeBilgiTaskCaseDbContext context) : base(context)
        {
        }
    }
}
