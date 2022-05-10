using Microsoft.Extensions.DependencyInjection;
using NSE.Identity.API.Controllers;

namespace NSE.Identity.API.Configuration {
    
    public static class DependencyInjectionConfig 
    {
        
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenHandler, TokenHandler>();
        }
    }
}