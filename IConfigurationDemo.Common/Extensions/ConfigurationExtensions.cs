using Microsoft.Extensions.Configuration;

namespace IConfigurationDemo.Common.Extensions;

public static class ConfigurationExtensions
{
    public static T GetRequired<T>(this IConfiguration configuration) =>
        configuration.Get<T>() ?? throw new InvalidOperationException($"{typeof(T).Name} isn't registered."); 
}