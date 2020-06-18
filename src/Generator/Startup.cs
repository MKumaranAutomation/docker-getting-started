using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Generator
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var swaggerConfig = Configuration.GetSection("SwaggerConfiguration");
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc(
                    swaggerConfig["ApiName"],
                    new OpenApiInfo
                    {
                        Title = swaggerConfig["Title"],
                        Version = swaggerConfig["Version"],
                        Description =
                            swaggerConfig["Description"],
                        Contact = new OpenApiContact
                        {
                            Name = swaggerConfig["ContactName"],
                            Url = new Uri(swaggerConfig["ContactUrl"])
                        }
                    });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            var swaggerConfig = Configuration.GetSection("SwaggerConfiguration");
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{swaggerConfig["ApiName"]}/swagger.json",
                    $"{swaggerConfig["Title"]} v{swaggerConfig["Version"]}");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
