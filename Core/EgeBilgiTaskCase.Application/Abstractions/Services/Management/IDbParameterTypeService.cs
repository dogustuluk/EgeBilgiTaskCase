using EgeBilgiTaskCase.Application.Common.DTOs.Management;
using EgeBilgiTaskCase.Domain.Entities.Management;

namespace EgeBilgiTaskCase.Application.Abstractions.Services.Management
{
    public interface IDbParameterTypeService
    {
        Task<OptResult<DbParameterType>> CreateDbParameterTypeAsync(Create_DBParameterType_Dto model);
        Task<OptResult<DbParameterType>> UpdateDbParameterTypeAsync(Update_DBParameterType_Dto model);
        Task<OptResult<DbParameterType>> GetByIdOrGuid(object criteria);
        Task<List<DbParameterType>> GetAllDbParameterTypeAsync(Expression<Func<DbParameterType, bool>>? predicate, string? include);
        Task<OptResult<PaginatedList<DbParameterType>>> GetAllPagedDbParameterTypeAsync(GetAllPaged_DBParameterType_Index_Dto model);
        Task<string> GetValue(string? table, string column, string sqlQuery, int? dbType);
        Task<List<DataList1>> GetDataListAsync(int? itemType);

    }
}
