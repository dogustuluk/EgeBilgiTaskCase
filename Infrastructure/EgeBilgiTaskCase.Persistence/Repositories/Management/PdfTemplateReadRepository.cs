using EgeBilgiTaskCase.Application.Repositories.Management;
using EgeBilgiTaskCase.Domain.Entities.Management;
using EgeBilgiTaskCase.Persistence.Context;

namespace EgeBilgiTaskCase.Persistence.Repositories.Management
{
    public class PdfTemplateReadRepository : ReadRepository<PdfTemplate>, IPdfTemplateReadRepository
    {
        public PdfTemplateReadRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
