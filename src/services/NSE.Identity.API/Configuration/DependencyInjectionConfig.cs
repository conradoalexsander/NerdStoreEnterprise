using Microsoft.Extensions.DependencyInjection;
using NSE.Identity.API.Controllers;
using NSE.Identity.API.Services;

namespace NSE.Identity.API.Configuration {
    
    public static class DependencyInjectionConfig 
    {
        
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthService, AuthService>();
        }
    }
}