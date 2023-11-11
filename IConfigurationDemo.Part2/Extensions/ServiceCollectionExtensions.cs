using IConfigurationDemo_Part2;
using IConfigurationDemo.Common.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IConfigurationDemo.Part2.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAdminSettings(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.Configure<DiscountSettings>(configuration.GetSection(ConfigurationKeys.Discount));
        
        return serviceCollection;
    }
}

