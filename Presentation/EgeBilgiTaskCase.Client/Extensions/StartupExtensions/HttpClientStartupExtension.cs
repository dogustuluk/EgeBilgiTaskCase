using EgeBilgiTaskCase.Client.Services;

namespace EgeBilgiTaskCase.Client.Extensions.StartupExtensions
{
    public static class HttpClientStartupExtension
    {
        public static void HttpClientOptionsStartupExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient("MyApiClient", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7083/api");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            services.AddScoped<IHttpClientService>(provider =>
            {
                var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
                var configuration = provider.GetRequiredService<IConfiguration>();
                var baseUrl = configuration.GetValue<string>("HttpClientSettings:BaseAddress");
                return new HttpClientService(httpClientFactory, baseUrl);
            });
        }
    }
}
