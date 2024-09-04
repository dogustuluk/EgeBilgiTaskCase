using EgeBilgiTaskCase.Application.Abstractions.Services.Character;
using EgeBilgiTaskCase.Domain.Entities.Identity;
using EgeBilgiTaskCase.Persistence;
using EgeBilgiTaskCase.Persistence.Context;
using EgeBilgiTaskCase.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HospitalManagement.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<EgeBilgiTaskCaseDbContext>(options => options.UseSqlServer(Configuration.ConnectionString));

            services.AddIdentity<AppUser, AppRole>(options =>
            {
                //for test mode
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            }).AddEntityFrameworkStores<EgeBilgiTaskCaseDbContext>();//identity
            

            services.RegisterRepositories(typeof(IErrorReadRepository).Assembly, typeof(ErrorReadRepository).Assembly);
            services.AddServicesInDbContextFromAttributes(Assembly.GetExecutingAssembly());
            

        }

        public static void InitializeSeedData(this IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<EgeBilgiTaskCaseDbContext>();
              //  SeedDataHelper.SeedDepartments(dbContext);
              //  SeedDataHelper.SeedCities(dbContext);
            }
        }

    }
}
