using EgeBilgiTaskCase.Application.Abstractions.Caching;
using EgeBilgiTaskCase.Application.Abstractions.Storage;
using EgeBilgiTaskCase.Application.Abstractions.Token;
using EgeBilgiTaskCase.Application.Services;
using EgeBilgiTaskCase.Infrastructure.Services;
using EgeBilgiTaskCase.Infrastructure.Services.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace EgeBilgiTaskCase.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddScoped<IFileService, FileService>();
            serviceCollection.AddScoped<IStorageService, StorageService>();
            serviceCollection.AddScoped<ITokenHandler, TokenHandler>();
            serviceCollection.AddScoped<IRedisCacheService, RedisCacheService>();

            serviceCollection.AddTransient<IConnectionMultiplexer>(sp =>
            {
                var options = new ConfigurationOptions
                {
                    EndPoints = { configuration.GetConnectionString("RedisConnection") },
                    AbortOnConnectFail = false,
                    AsyncTimeout = 10000,
                    ConnectTimeout = 10000

                };
                return ConnectionMultiplexer.Connect(options);
            });
        }
        public static void AddStorage<T>(this IServiceCollection serviceCollection) where T : Storage, IStorage
        {
            serviceCollection.AddScoped<IStorage, T>();
        }
    }
}
