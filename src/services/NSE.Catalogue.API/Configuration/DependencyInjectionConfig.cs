using Microsoft.Extensions.DependencyInjection;
using NSE.Catalogue.API.Data;
using NSE.Catalogue.API.Data.Repository;
using NSE.Catalogue.API.Models;

namespace NSE.Catalogue.API.Configuration {
    public static class DependencyInjectionConfig {
        public static void RegisterServices(this IServiceCollection services) {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<CatalogueContext>();
        }
    }
}