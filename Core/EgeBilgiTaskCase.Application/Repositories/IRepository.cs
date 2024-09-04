using EgeBilgiTaskCase.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace EgeBilgiTaskCase.Application.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        DbSet<T> Table { get; }
    }
    
}
