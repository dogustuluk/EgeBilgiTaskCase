using Microsoft.AspNetCore.Http.Features;

namespace EgeBilgiTaskCase.Client.Extensions.StartupExtensions
{
    public static class IISStartupExtension
    {
        public static void IISOptionsStartupExtension(this IServiceCollection services)
        {
            services.Configure<IISServerOptions>(options => { options.MaxRequestBodySize = int.MaxValue; });
            services.Configure<FormOptions>(options =>
            {
                options.ValueLengthLimit = int.MaxValue;
                options.MultipartBodyLengthLimit = int.MaxValue;
                options.MultipartHeadersLengthLimit = int.MaxValue;
            });
        }
    }
}
