using Ecom.Core.Interfaces;
using Ecom.Core.Services;
using Ecom.Infrastructure.Data;
using Ecom.Infrastructure.Repositries;
using Ecom.Infrastructure.Repositries.service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure
{
    public static class InfrastructureRegistration
    {
        public static IServiceCollection InfrastructureConfiguration(this IServiceCollection services , IConfiguration  configuration )
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
           
        
            //Apply Unit Of Work
            services.AddScoped<IUnitOfWork ,UnitOfWork>();

            services.AddDbContext<AppDbContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("EcomDatabase"));
            });

            services.AddSingleton<IimageManagementSerives, ImageManagementService>();
            
            services.AddSingleton<IFileProvider>(
                new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));

            return services;
        }


    }
}
