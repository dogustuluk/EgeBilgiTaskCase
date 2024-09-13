using EgeBilgiTaskCase.Application.Repositories.Common;
using EgeBilgiTaskCase.Domain.Entities.Common;
using EgeBilgiTaskCase.Persistence.Context;

namespace EgeBilgiTaskCase.Persistence.Repositories.Common
{
    public class StatusReadRepository : ReadRepository<Status>, IStatusReadRepository
    {
        public StatusReadRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
