using AspNetCoreRateLimit;
using HotelListingAPI.Configurations;
using HotelListingAPI.Data;
using HotelListingAPI.IRepository;
using HotelListingAPI.Repository;
using HotelListingAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListingAPI
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
            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("sqlConnection"))
            );

            services.AddMemoryCache(); // rate limiting
            services.ConfigureRateLimiting();
            services.AddHttpContextAccessor(); 


            //Setup Identity Core Services
            services.ConfigureHttpCacheHeaders(); 

            services.AddAuthentication();
            services.CustomConfigureIdentity();
            services.ConfigureJWT(Configuration);

            services.AddCors(c => {
                c.AddPolicy("CorsAllowAllPolicy", builder =>
                builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            services.AddAutoMapper(typeof(MapperInitilizer));

            // transient = every time it is needed, a new instance is created
            // scoped = lifetime of request
            // only 1 will exist for entire duration of app

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthManager, AuthManager>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HotelListingAPI", Version = "v1" });
            });

            services.AddControllers(config => {
                config.CacheProfiles.Add("Cache120s", new CacheProfile 
                    {
                        Duration = 120 
                    });
                })
                .AddNewtonsoftJson(opt => 
                     opt.SerializerSettings.ReferenceLoopHandling =
                        Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.ConfigureVersioning();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();               
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HotelListingAPI v1"));

            app.ConfigureExceptionHandler();

            app.UseHttpsRedirection();

            app.UseCors("CorsAllowAllPolicy");

            app.UseResponseCaching();
            app.UseHttpCacheHeaders();
            app.UseIpRateLimiting();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
               endpoints.MapControllers();
            });
        }
    }
}
