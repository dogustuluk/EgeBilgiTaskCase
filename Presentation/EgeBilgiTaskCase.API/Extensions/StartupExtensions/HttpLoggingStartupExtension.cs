using Microsoft.AspNetCore.HttpLogging;

namespace HospitalManagement.API.Extensions.StartupExtensions
{
    public static class HttpLoggingStartupExtension
    {
        public static void HttpLoggingOptionsStartupExtension(this IServiceCollection services)
        {
            services.AddHttpLogging(logging =>
            {
                logging.LoggingFields = HttpLoggingFields.All;
                logging.RequestHeaders.Add("sec-ch-ua"); //kullanıcıya dair tüm teferrüatlı bilgileri getirir.
                logging.MediaTypeOptions.AddText("application/javascript");
                logging.RequestBodyLogLimit = 4096;
                logging.ResponseBodyLogLimit = 4096;
            });
        }
    }
}
