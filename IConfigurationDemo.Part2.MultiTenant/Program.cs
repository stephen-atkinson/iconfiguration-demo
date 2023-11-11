using IConfigurationDemo.Common.Models;
using IConfigurationDemo.Part2.MultiTenant.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace IConfigurationDemo.Part2.MultiTenant;

public static class Program
{
    public static void Main(string[] args)
    {
        var adminApiSettingsData = new Dictionary<string, string?>
        {
            { "AdminApi:Url", "https://mocki.io/v1/1cf1a89f-2279-45fa-a357-3b731a160d6d" },
        };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(adminApiSettingsData)
            .AddAdminSettings()
            .Build();

        var serviceProvider = new ServiceCollection()
            .AddAdminSettings(configuration)
            .BuildServiceProvider();

        var options = serviceProvider
            .GetRequiredService<IOptionsSnapshot<DiscountSettings>>();
            
         var leedsCafe = options.Get("LeedsCafe");
         var manchesterCafe = options.Get("ManchesterCafe");
        
        Console.WriteLine($"Leeds Discount Enabled: {leedsCafe.Food.Enabled}");
        Console.WriteLine($"Leeds Discount Percent: {leedsCafe.Food.Percent}");
        
        Console.WriteLine($"Manchester Discount Enabled: {manchesterCafe.Food.Enabled}");
        Console.WriteLine($"Manchester Discount Percent: {manchesterCafe.Food.Percent}");
    }
}