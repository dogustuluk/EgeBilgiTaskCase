using EgeBilgiTaskCase.Persistence.Context;

namespace EgeBilgiTaskCase.Persistence.Repositories.Common;
public class FFileWriteRepository : WriteRepository<FFile>, IFFileWriteRepository
{
    public FFileWriteRepository(ApplicationDbContext context) : base(context)
    {
    }
}
