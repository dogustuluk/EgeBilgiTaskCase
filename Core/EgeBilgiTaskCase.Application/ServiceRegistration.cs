using EgeBilgiTaskCase.Application.Common.Specifications;
using EgeBilgiTaskCase.Application.Common.Validators;

namespace HospitalManagement.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection serviceCollection)
        {

            serviceCollection.AddMediatR(typeof(ServiceRegistration));
            serviceCollection.AddHttpClient();
            serviceCollection.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly()); 
            serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
            serviceCollection.AddAutoMapper(Assembly.GetExecutingAssembly());

            //otomatize et -->
            
            serviceCollection.AddScoped<CharacterSpecifications>();
            serviceCollection.AddScoped<DbParameterSpecifications>();
            serviceCollection.AddScoped<DbParameterTypeSpecifications>();
            serviceCollection.AddScoped<ErrorSpecifications>();
            serviceCollection.AddScoped<UserSpecifications>();
            
           // serviceCollection.AddScoped<ICryptographyService,CryptographyService>();

        }
    }
}