using Microsoft.EntityFrameworkCore;
using WellDesignedAPI.EntityFramework.DbContexts;

namespace WellDesignedAPI.Web.Host.Startup.Extensions
{
    public static partial class ConfigureEntityFrameworkDb
    {
        public static void ConfigureDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => {
                options.UseSqlServer(configuration["ConnectionStrings:WellDesignedAPIMain"], providerOptions => providerOptions.EnableRetryOnFailure());
            });
        }
    }
}
