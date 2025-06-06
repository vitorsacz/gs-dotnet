using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AlertaCidadao.Application;
using AlertaCidadao.Application.Services;
using AlertaCidadao.Application.Services.Interfaces;
using AlertaCidadao.Infraestructure.Data.AppData;
using AlertaCidadao.Infraestructure.Data.Repositories;
using AlertaCidadao.Infraestructure.Data.Repositories.Interfaces;
using AlertaCidadao.Infraestructure.Mappings;

namespace AlertaCidadao.Ioc;

public class Bootstrap
{
    public static void Start(IServiceCollection service, IConfiguration configuration)
    {
        service.AddDbContext<ApplicationContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("Oracle");
            options.UseOracle(connectionString);
        });

        service.AddScoped<IUserRepository, UserRepository>();
        service.AddScoped<IUserService, UserService>();

        service.AddScoped<IClimaticEventRepository, ClimaticEventRepository>();
        service.AddScoped<IClimaticEventService, ClimaticEventService>();

        service.AddScoped<ISafeResourceRepository, SafeResourceRepository>();
        service.AddScoped<ISafeResourceService, SafeResourceService>();

        service.AddScoped<ISentimentAnalysisService, SentimentAnalysisService>();

        service.AddAutoMapper(typeof(MapperProfile));
    }
}
