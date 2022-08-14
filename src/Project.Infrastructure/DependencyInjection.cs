using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project.ApplicationCore.DTOs;
using Project.ApplicationCore.Entities.Models;
using Project.ApplicationCore.Exceptions;
using Project.ApplicationCore.Helper;
using Project.ApplicationCore.Interfaces;
using Project.ApplicationCore.Mappings;
using Project.Infrastructure.Persistence.Contexts;
using Project.Infrastructure.Persistence.Repositories;
namespace Project.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var defaultConnectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<StationsContext>(options =>
            options.UseSqlServer(defaultConnectionString));
            services.AddScoped<IGetStationRepository, GetStationRepository>();
            services.AddScoped<ICSVDataImportRepository, CSVDataImportRepository>();
            services.AddSingleton<ILoggerManager, LoggerManager>();
            services.AddScoped<IReaderWrapperRepository, ReaderWrapperRepository>();
            services.AddScoped<IBikeStationRepository, BikeStationRepository>();
            services.AddScoped<ISortHelper<BikeStation>, SortHelper<BikeStation>>();
            services.AddScoped<IJourneyListRepository, JourneyListRepository>();
            var config = new AutoMapper.MapperConfiguration(c =>
            {
                c.AddProfile(new GeneralProfile());
            });
            var mapper =config.CreateMapper();
            services.AddSingleton(mapper);
            return services;
        }
    }
}
