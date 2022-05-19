using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using NSE.Catalogue.API.Data;

namespace NSE.Catalogue.API.Configuration {
    public static class SwaggerConfig {
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(gen =>
            {
                gen.SwaggerDoc(name: "v1", new OpenApiInfo
                {
                    Title = "NerdStore Enterprise Catalogue API",
                    Description = "This API is part of ASP.NET Core Enterprise Application course",
                    Contact = new OpenApiContact() { Name = "Conrado Alexsander" },
                    License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
                });
            });
        }

        public static void UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(ui =>
            {
                ui.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "v1");
            });
        }
    }
}