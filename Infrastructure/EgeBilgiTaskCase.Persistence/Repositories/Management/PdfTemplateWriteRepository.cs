using EgeBilgiTaskCase.Application.Repositories.Management;
using EgeBilgiTaskCase.Domain.Entities.Management;
using EgeBilgiTaskCase.Persistence.Context;

namespace EgeBilgiTaskCase.Persistence.Repositories.Management
{
    public class PdfTemplateWriteRepository : WriteRepository<PdfTemplate>, IPdfTemplateWriteRepository
    {
        public PdfTemplateWriteRepository(EgeBilgiTaskCaseDbContext context) : base(context)
        {
        }
    }
}
