using EgeBilgiTaskCase.Persistence.Context;

namespace EgeBilgiTaskCase.Persistence.Repositories.Common
{
    public class ErrorWriteRepository : WriteRepository<Error>, IErrorWriteRepository
    {
        public ErrorWriteRepository(EgeBilgiTaskCaseDbContext context) : base(context)
        {
        }
    }
}
