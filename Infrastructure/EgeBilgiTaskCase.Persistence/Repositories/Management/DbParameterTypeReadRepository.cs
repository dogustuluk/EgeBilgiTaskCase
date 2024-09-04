using EgeBilgiTaskCase.Application.Repositories.Common;
using EgeBilgiTaskCase.Domain.Entities.Management;
using EgeBilgiTaskCase.Persistence.Context;

namespace EgeBilgiTaskCase.Persistence.Repositories.Common
{
    public class DbParameterTypeReadRepository : ReadRepository<DbParameterType>, IDbParameterTypeReadRepository
    {
        public DbParameterTypeReadRepository(EgeBilgiTaskCaseDbContext context) : base(context)
        {
        }
    }
}
