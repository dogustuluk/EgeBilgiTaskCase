using EgeBilgiTaskCase.Application.Abstractions.Token;
using EgeBilgiTaskCase.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EgeBilgiTaskCase.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddScoped<ITokenHandler, TokenHandler>();

            //serviceCollection.AddTransient<IConnectionMultiplexer>(sp =>
            //{
            //    var options = new ConfigurationOptions
            //    {
            //        EndPoints = { configuration.GetConnectionString("RedisConnection") },
            //        AbortOnConnectFail = false,
            //        AsyncTimeout = 10000,
            //        ConnectTimeout = 10000

            //    };
            //    return ConnectionMultiplexer.Connect(options);
            //});
          //  serviceCollection.AddScoped<IRedisCacheService, RedisCacheService>();

        }
    }
}
