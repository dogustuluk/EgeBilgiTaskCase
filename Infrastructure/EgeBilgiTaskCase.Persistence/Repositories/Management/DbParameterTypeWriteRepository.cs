using EgeBilgiTaskCase.Application.Repositories.Common;
using EgeBilgiTaskCase.Domain.Entities.Management;
using EgeBilgiTaskCase.Persistence.Context;

namespace EgeBilgiTaskCase.Persistence.Repositories.Common
{
    public class DbParameterTypeWriteRepository : WriteRepository<DbParameterType>, IDbParameterTypeWriteRepository
    {
        public DbParameterTypeWriteRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
