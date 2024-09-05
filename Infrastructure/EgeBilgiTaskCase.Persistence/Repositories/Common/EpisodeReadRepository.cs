using EgeBilgiTaskCase.Domain.Entities.Character;
using EgeBilgiTaskCase.Persistence.Context;

namespace EgeBilgiTaskCase.Persistence.Repositories.Common
{
    public class EpisodeReadRepository : ReadRepository<Episode>, IEpisodeReadRepository
    {
        public EpisodeReadRepository(EgeBilgiTaskCaseDbContext context) : base(context)
        {
        }
    }
}
