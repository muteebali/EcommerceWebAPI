using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace OnlinePOSAPI.Extensions
{
    public static class SwaggerExtension
    {
        public static void AddSwaggerConfig(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options => {
                options.SwaggerEndpoint("../swagger/v1/swagger.json", "SwaggerUI");
            });
        }

        public static  void AddSwaggerContext(this IServiceCollection services)
        {
            services.AddSwaggerGen();
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "POS Swagger Doc"
                    });
            });
        }
    }
}
