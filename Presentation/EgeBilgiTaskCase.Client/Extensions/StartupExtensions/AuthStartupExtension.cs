namespace EgeBilgiTaskCase.Client.Extensions.StartupExtensions
{
    public static class AuthStartupExtension
    {
        public static void AuthOptionsStartupExtension(this IServiceCollection services)
        {
            services.AddAntiforgery(x => x.HeaderName = "MEDSIS-TOKEN");

            services.AddAuthentication("MedsisUser").AddCookie("MedsisUser", options =>
            {
                options.Cookie.Name = "MedsisUser";
                options.ExpireTimeSpan = TimeSpan.FromDays(5);
                options.SlidingExpiration = true;
                options.LoginPath = "/Auth/Login";
                options.LogoutPath = "/Auth/Logout";
                options.AccessDeniedPath = "/Auth/AccessDenied";
            });

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = false;
                options.Cookie.IsEssential = true;
            });
        }
    }
}
