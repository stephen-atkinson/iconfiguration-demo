using IConfigurationDemo.Common.Models;
using IConfigurationDemo.Part2.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace IConfigurationDemo.Part2;

public static class Program
{
    public static void Main(string[] args)
    {
        var adminApiSettingsData = new Dictionary<string, string?>
        {
            { "AdminApi:Url", "https://mocki.io/v1/f1e61514-1947-483f-a0aa-b5cccad9b8c7" },
        };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(adminApiSettingsData)
            .AddAdminSettings()
            .Build();

        var serviceProvider = new ServiceCollection()
            .AddAdminSettings(configuration)
            .BuildServiceProvider();

        var options = serviceProvider
            .GetRequiredService<IOptions<DiscountSettings>>()
            .Value;
        
        Console.WriteLine($"Discount Enabled: {options.Food.Enabled}");
        Console.WriteLine($"Discount Percent: {options.Food.Percent}");
    }
}