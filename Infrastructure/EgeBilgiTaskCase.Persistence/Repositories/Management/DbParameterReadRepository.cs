using EgeBilgiTaskCase.Application.Repositories.Common;
using EgeBilgiTaskCase.Domain.Entities.Management;
using EgeBilgiTaskCase.Persistence.Context;

namespace EgeBilgiTaskCase.Persistence.Repositories.Common
{
    public class DbParameterReadRepository : ReadRepository<DbParameter>, IDbParameterReadRepository
    {
        public DbParameterReadRepository(EgeBilgiTaskCaseDbContext context) : base(context)
        {
        }
    }
}
