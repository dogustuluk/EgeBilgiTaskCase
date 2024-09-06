using System.Text.Json.Serialization;

namespace EgeBilgiTaskCase.Client.Extensions.StartupExtensions
{
    public static class JsonStartupExtension
    {
        public static void JsonOptionsStartupExtension(this IServiceCollection services)
        {
            services.AddControllersWithViews()
                .AddSessionStateTempDataProvider()
                .AddJsonOptions(opt =>
                {
                    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    opt.JsonSerializerOptions.PropertyNamingPolicy = null;
                });
        }
    }
}
