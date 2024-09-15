using DelmarV2.Domain.Entities.Common;
using EgeBilgiTaskCase.Persistence.Context;

namespace EgeBilgiTaskCase.Persistence.Repositories.Common;
public class FFileWriteRepository : WriteRepository<FFile>, IFFileWriteRepository
{
    public FFileWriteRepository(ApplicationDbContext context) : base(context)
    {
    }
}
