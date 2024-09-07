using EgeBilgiTaskCase.Application.Common.DTOs.Management;
using EgeBilgiTaskCase.Domain.Entities.Management;

namespace EgeBilgiTaskCase.Application.Abstractions.Services.Management
{
    public interface IDbParameterService
    {
        Task<OptResult<DbParameter>> CreateDbParameterAsync(Create_DBParameter_Dto model);
        Task<OptResult<DbParameter>> UpdateDbParameterAsync(Update_DBParameter_Dto model);
        Task<OptResult<DbParameter>> GetByIdOrGuid(object criteria);
        Task<List<DbParameter>> GetAllDbParameterAsync(Expression<Func<DbParameter, bool>>? predicate, string? include);
        Task<OptResult<PaginatedList<DbParameter>>> GetAllPagedDbParameterAsync(GetAllPaged_DBParameter_Index_Dto model);
        Task<string> GetValue(string? table, string column, string sqlQuery, int? dbType);
        Task<List<DataList1>> GetDataListAsync(int? dbParameterTypeId, int? parentId);
        Task<bool> ExistsDbParameterAsync(Expression<Func<DbParameter, bool>> predicate);
        Task<DbParameter> GetEntity(Expression<Func<DbParameter, bool>> method, bool tracking = true);
    }
}
