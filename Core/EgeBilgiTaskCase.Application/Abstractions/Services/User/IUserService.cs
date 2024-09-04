using EgeBilgiTaskCase.Application.Common.DTOs.User;
using EgeBilgiTaskCase.Domain.Entities.Identity;

namespace HospitalManagement.Application.Abstractions.Services.Users
{
    public interface IUserService
    {
        Task<OptResult<CreateUser_Dto>> CreateAsync(CreateUser_Dto model);
        Task<OptResult<UpdateUser_Dto>> UpdateAsync(UpdateUser_Dto model);
        Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate);

        Task<List<AppUser>> GetAllUser(Expression<Func<AppUser, bool>>? predicate, string? include);
        Task<List<DataList1>> GetDataListAsync(int? filter);
        Task<OptResult<AppUser>> GetByIdOrGuid(object criteria);
        Task<OptResult<PaginatedList<AppUser>>> GetAllPagedListAsync(GetAllPagedUser_Index_Dto model);
        Task<string> GetValue(string? table, string column, string sqlQuery, int? dbType);
    }
}
