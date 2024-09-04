using EgeBilgiTaskCase.Application.Repositories.Common;
using EgeBilgiTaskCase.Domain.Entities.Management;
using EgeBilgiTaskCase.Persistence.Context;

namespace EgeBilgiTaskCase.Persistence.Repositories.Common
{
    public class DbParameterWriteRepository : WriteRepository<DbParameter>, IDbParameterWriteRepository
    {
        public DbParameterWriteRepository(EgeBilgiTaskCaseDbContext context) : base(context)
        {
        }
    }
}
