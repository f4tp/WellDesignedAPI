using WellDesignedAPI.Web.Host.Middleware;

namespace WellDesignedAPI.Web.Host.Startup.Extensions
{
    public static partial class GlobalExceptionHandlingConfig
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
