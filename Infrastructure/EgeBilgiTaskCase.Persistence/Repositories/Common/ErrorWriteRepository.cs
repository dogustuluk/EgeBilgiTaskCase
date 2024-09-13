using EgeBilgiTaskCase.Persistence.Context;

namespace EgeBilgiTaskCase.Persistence.Repositories.Common
{
    public class ErrorWriteRepository : WriteRepository<Error>, IErrorWriteRepository
    {
        public ErrorWriteRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
