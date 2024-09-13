using EgeBilgiTaskCase.Domain.Entities.Character;
using EgeBilgiTaskCase.Persistence.Context;

namespace EgeBilgiTaskCase.Persistence.Repositories.Common
{
    public class LocationReadRepository : ReadRepository<Location>, ILocationReadRepository
    {
        public LocationReadRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
