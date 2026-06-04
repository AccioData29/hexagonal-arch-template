using Company.ProjectName.Host.Extensions;

namespace Company.ProjectName.Host;

public static class HostExtensions
{
    public static void ConfigurationService(IServiceCollection services, IConfiguration configuration)
    {
        services.AddCustomMvc()
                .AddAutoMapperService()
                .AddDependencyInjection()
                .AddEntityFrameworkCore(configuration)
                .AddCustomCors()
                .AddCustomSwagger()
                .AddHttpContextAccessor();
    }
}
