using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnlinePOSAPI.Extensions;
using OnlinePOSAPI.FilterAttribute;
using OnlinePOSAPI.Models;

namespace OnlinePOSAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerContext();
            services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
            services.AddScoped<ValidationFilterAttribute>();
            services.AddAutoMapper(typeof(ModelMapping));
            services.AddDbContext<OnlinePOSContext>(options =>
                                                    options.UseSqlServer(Configuration.GetConnectionString("POSConnection")));
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.WithOrigins("https://localhost:44384")
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            services.AddControllers(options =>
                                    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true)
                                        .AddNewtonsoftJson();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
           
            app.AddSwaggerConfig();

            //app.Map("/test", (app) =>
            //{
            //    app.Use(async (context, next) => {
            //        await context.Response.WriteAsync("Custom Middleware 1\n");
            //        await next();

            //    });
            //    app.Use(async (context, next) => {
            //        await context.Response.WriteAsync("Custom Middleware 2\n");
            //    });
            //});
            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("middelware use 1\n");
            //    await next();
            //    await context.Response.WriteAsync("middelware use 2\n");
            //    await next();
            //});

            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("middleware use 3\n");
            //    await next();
            //});

            //app.Run(async handler =>
            //{
            //    await handler.Response.WriteAsync("middleware run 1 \n");
            //});

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseCors("MyPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
