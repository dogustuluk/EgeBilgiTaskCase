using EgeBilgiTaskCase.Persistence.Context;

namespace EgeBilgiTaskCase.Persistence.Repositories.Common
{
    public class ErrorReadRepository : ReadRepository<Error>, IErrorReadRepository
    {
        public ErrorReadRepository(EgeBilgiTaskCaseDbContext context) : base(context)
        {
        }
    }
}
