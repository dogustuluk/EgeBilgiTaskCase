using EgeBilgiTaskCase.Domain.Entities.Identity;

namespace HospitalManagement.Application.Repositories
{
    public interface IAppUserWriteRepository
    {
        Task<bool> AddAsync(AppUser model);
        Task<bool> AddRangeAsync(List<AppUser> datas);

        bool Remove(AppUser model);
        bool RemoveRange(List<AppUser> datas);
        Task<bool> RemoveAsync(int id);

        Task<bool> UpdateAsync(AppUser model);
        bool Update(AppUser model);

        Task<int> SqlCommandAsync(string sql, params object[]? parameters);
        int SqlCommand(string sql, params object[]? parameters);

        Task<int> SaveChanges();
    }
}