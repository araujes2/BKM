using BKM.API.Utilities;
using BKM.Core.Interfaces;
using BKM.Infrastructure.EntityFramework;
using EDAP.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace BKM.API
{
    public static class ServiceConfigurationExtensions
    {
        public static IServiceCollection AddRepository(this IServiceCollection services, ServiceOptions options)
        {
            var dbOptions = new DbContextOptionsBuilder<BKMContext>()
             .UseSqlServer(options.SqlConnectionString)
             .Options;

            services.AddScoped<IRepositoryProvider>(s => new RepositoryProvider(dbOptions));
            return services;
        }

        public static IServiceCollection RegisterRequestHandlers(this IServiceCollection services)
        {
            return services.AddMediatR(typeof(ServiceConfigurationExtensions).Assembly);
        }

        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, ServiceOptions options)
        {

            services.AddAuthorization(options =>
            {
                options.AddPolicy("user", policy => policy.RequireClaim("Store", "user"));
            });

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(TokenHelper.Secret),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            return services;
        }


    }
}
