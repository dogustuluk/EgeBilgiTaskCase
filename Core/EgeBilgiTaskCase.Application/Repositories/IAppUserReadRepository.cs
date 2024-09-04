using EgeBilgiTaskCase.Domain.Entities.Identity;
using System.Linq.Expressions;

namespace EgeBilgiTaskCase.Application.Repositories
{
    public interface IAppUserReadRepository
    {
        Task<IQueryable<AppUser>> GetAllAsync(Expression<Func<AppUser, bool>>? predicate, string? include);
        Task<List<AppUser>> GetAllSqlAsync(string table, string sqlQuery, string? include);

        Task<IQueryable<AppUser>> GetDataAsync(Expression<Func<AppUser, bool>> predicate, string? include, int take, string orderBy);
        Task<List<AppUser>> GetDataSqlAsync(string table, string sqlQuery, string? include, int pageIndex, int take, string orderBy);

        Task<PaginatedList<AppUser>> GetDataPagedAsync(Expression<Func<AppUser, bool>> predicate, string? include, int pageIndex, int take, string orderBy, bool? isTrack = false);
        Task<PaginatedList<AppUser>> GetDataPagedSqlAsync(string table, string sqlQuery, string? include, int pageIndex, int take, string orderBy);

        Task<IQueryable<AppUser>> GetSortedDataAsync(IQueryable<AppUser> query, string orderBy);
        IQueryable<AppUser> GetWhere(Expression<Func<AppUser, bool>> predicate, bool tracking = true);

        Task<AppUser> GetSingleEntityAsync(Expression<Func<AppUser, bool>> method, bool tracking = true);
        Task<AppUser> GetByEntityAsync(object value, string fieldName = null);
        Task<AppUser> GetByIdAsync(int id);
        Task<AppUser> GetByGuidAsync(Guid guid);
        Task<AppUser> GetEntityWithIncludeAsync(Expression<Func<AppUser, bool>> predicate, string? include);
        string? GetValue(string table, string column, string sqlQuery);
        Task<string?> GetValueAsync(string table, string column, string sqlQuery, int dbType);

        Task<bool> ExistsAsync(Expression<Func<AppUser, bool>> predicate);
        Task<int> CountAsync(Expression<Func<AppUser, bool>>? predicate);
    }
}
