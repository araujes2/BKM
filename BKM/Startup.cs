using BKM.API;
using BKM.API.Utilities;
using BKM.Core.Interfaces;
using BKM.Infrastructure.EntityFramework;
using EDAP.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace BKM.API
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
            var serviceConfiguration = Configuration.GetSection(ServiceOptions.Configurations);

            var options = serviceConfiguration.Get<ServiceOptions>();

            var dbOptions = new DbContextOptionsBuilder<BKMContext>()
               .UseSqlServer(options.SqlConnectionString)
               .Options;

            services.AddScoped<IRepositoryProvider>(s => new RepositoryProvider(dbOptions));

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddAutoMapper(typeof(Startup));

            services.AddMemoryCache();

            services.AddScoped<QueueMessageActionFilter>();

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
