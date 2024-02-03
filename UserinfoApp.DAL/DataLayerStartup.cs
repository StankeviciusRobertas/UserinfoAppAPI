using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserinfoApp.DAL.Repositories;
using UserinfoApp.DAL.Repositories.Interfaces;

namespace UserinfoApp.DAL
{
    public static class DataLayerStartup
    {
        public static void ConfigureDataLayerServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UserinfoAppDbContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("Database"));
            });

            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<IUserinfoRepository, UserinfoRepository>();
            services.AddScoped<IUserAdressRepository, UserAdressRepository>();
        }
    }
}
