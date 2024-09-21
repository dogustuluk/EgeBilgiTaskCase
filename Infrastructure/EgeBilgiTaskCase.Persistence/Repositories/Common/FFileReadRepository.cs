using EgeBilgiTaskCase.Persistence.Context;

namespace EgeBilgiTaskCase.Persistence.Repositories.Common;
public class FFileReadRepository : ReadRepository<FFile>, IFFileReadRepository
{
    public FFileReadRepository(ApplicationDbContext context) : base(context)
    {
    }
}
