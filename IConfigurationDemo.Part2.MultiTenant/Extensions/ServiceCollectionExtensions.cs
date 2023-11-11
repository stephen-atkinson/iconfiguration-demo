using IConfigurationDemo_Part2;
using IConfigurationDemo.Common.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IConfigurationDemo.Part2.MultiTenant.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAdminSettings(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        foreach (var tenantSection in configuration.GetSection(ConfigurationKeys.Tenant).GetChildren())
        {
            serviceCollection
                .Configure<DiscountSettings>(tenantSection.Key, tenantSection.GetSection(ConfigurationKeys.Discount));
        }
        
        return serviceCollection;
    }
}

