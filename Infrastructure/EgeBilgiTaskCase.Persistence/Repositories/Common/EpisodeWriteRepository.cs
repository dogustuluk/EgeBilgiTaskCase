using EgeBilgiTaskCase.Domain.Entities.Character;
using EgeBilgiTaskCase.Persistence.Context;

namespace EgeBilgiTaskCase.Persistence.Repositories.Common
{
    public class EpisodeWriteRepository : WriteRepository<Episode>, IEpisodeWriteRepository
    {
        public EpisodeWriteRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
