using HotelListingAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListingAPI
{
    public static class ServiceExtensions
    {
        public static void CustomConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<ApiUser>(u => u.User.RequireUniqueEmail = true);

            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), services);
            builder.AddEntityFrameworkStores<DatabaseContext>().AddDefaultTokenProviders();
        }

        //public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        //{
        //    var jwtSettings = configuration.GetSection("Jwt");
        //    var key = Environment.GetEnvironmentVariable("KEY"); // dont store this key in appSettings
        //    // what we set in command prompt 'setx KEY "5c0de99a-32fd-4c0d-b13f-c67c68697df0" /M

        //    services.AddAuthentication(opt =>
        //    {
        //        opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // Added auth to application, default scheme I want is Jwt.
        //        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //    })
        //    .AddJwtBearer(o =>
        //    {
        //        o.TokenValidationParameters = new TokenValidationParameters
        //        {
        //            // parameters we want to use to validate that this token should grant someone access
        //            ValidateIssuer = true, // is this "HotelListingAPI"?
        //            ValidateLifetime = true, // is it expired?
        //            ValidateIssuerSigningKey = true, // the KEY above is correct?
        //            ValidIssuer = jwtSettings.GetSection("Issuer").Value, // defined in our appSettings
        //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)) // gets encoded + hashed
        //        };
        //    });


        //}
        public static void ConfigureJWT(this IServiceCollection services, IConfiguration Configuration)
        {
            var jwtSettings = Configuration.GetSection("Jwt");
            var key = Environment.GetEnvironmentVariable("KEY");            
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.GetSection("Issuer").Value.ToString(),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                };
            });
        }
    }
}
