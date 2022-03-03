using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace ConsumeApis
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.WithOrigins("https://localhost:5001/", "http://")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            });
            services.AddHttpClient();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Consume API",
                    Version = "v1",
                    Description = "Description for the API goes here.",
                   
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
               
            }
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });
            app.UseRouting();
            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapGet("/", async context =>
                //{

                //    await context.Response.WriteAsync("Hello World!");
                //});
                //endpoints.MapControllerRoute(
                // name: "default",
                // pattern: "{controller=PhotoAlbum}/{action=MapPhotoAlbum}");
                //endpoints.MapControllerRoute(
                // name: "default",
                // pattern: "{controller=PhotoAlbum}/{action=GetPhotoDetails}");
                //endpoints.MapControllerRoute(
                // name: "default",
                // pattern: "{controller=PhotoAlbum}/{action=CsvButton}");
                endpoints.MapControllers();
            });
        //});
        }
    }
}
