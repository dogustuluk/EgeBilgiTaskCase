using EgeBilgiTaskCase.Domain.Entities.Identity;
using EgeBilgiTaskCase.Persistence.Context;
using HospitalManagement.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EgeBilgiTaskCase.Persistence.Repositories
{
    public class AppUserWriteRepository : IAppUserWriteRepository
    {
        private readonly ApplicationDbContext _context;

        public AppUserWriteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public DbSet<AppUser> Table => _context.Set<AppUser>();

        public async Task<bool> AddAsync(AppUser model)
        {
            EntityEntry<AppUser> entityEntry = await Table.AddAsync(model);
            return entityEntry.State == EntityState.Added;
        }

        public async Task<bool> AddRangeAsync(List<AppUser> datas)
        {
            await Table.AddRangeAsync(datas);
            return true;
        }

        public bool Remove(AppUser model)
        {
            EntityEntry<AppUser> entityEntry = Table.Remove(model);
            return entityEntry.State == EntityState.Deleted;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            AppUser model = await Table.FirstOrDefaultAsync(data => data.Id == id);
            return Remove(model);
        }

        public bool RemoveRange(List<AppUser> datas)
        {
            Table.RemoveRange(datas);
            return true;
        }

        public int SqlCommand(string sql, params object[]? parameters)
        {
            throw new NotImplementedException();
        }

        public Task<int> SqlCommandAsync(string sql, params object[]? parameters)
        {
            throw new NotImplementedException();
        }

        public bool Update(AppUser model)
        {
            EntityEntry entityEntry = Table.Update(model);
            return entityEntry.State == EntityState.Modified;
        }

        public Task<bool> UpdateAsync(AppUser model)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChanges()
            => _context.SaveChangesAsync();
    }
}
