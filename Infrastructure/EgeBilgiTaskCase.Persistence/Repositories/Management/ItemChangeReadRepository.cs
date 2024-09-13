using EgeBilgiTaskCase.Application.Repositories.Management;
using EgeBilgiTaskCase.Domain.Entities.Management;
using EgeBilgiTaskCase.Persistence.Context;

namespace EgeBilgiTaskCase.Persistence.Repositories.Management
{
    public class ItemChangeReadRepository : ReadRepository<ItemChange>, IItemChangeReadRepository
    {
        public ItemChangeReadRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
