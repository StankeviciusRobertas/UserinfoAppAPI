using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserinfoApp.BLL.Services.Interfaces;
using UserinfoApp.BLL.Services;

namespace UserinfoApp.BLL
{
    public static class BusinessLayerStartup
    {
        public static void ConfigureBusinessLayerServices(this IServiceCollection services)
        {
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IEmailService, EmailService>();
        }
    }
}
