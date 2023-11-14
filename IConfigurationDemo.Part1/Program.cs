using System;
using IConfigurationDemo.Common;
using IConfigurationDemo.Common.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace IConfigurationDemo.Part1;

public static class Program
{
    public static void Main(string[] args)
    {
        var environmentName = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");

        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .AddJsonFile($"appsettings.{environmentName}.json", optional: false, reloadOnChange: false)
            .AddEnvironmentVariables()
            .AddCommandLine(args)
            .Build();

        var badSettings = new DiscountSettings
        {
            Food = new ItemTypeDiscountSettings
            {
                Enabled = configuration.GetValue<bool>("Discount:Food:Enabled"),
                Percent = configuration.GetValue<decimal>("Discount:Food:Percent")
            }
        };

        var goodSettings = configuration
            .GetRequiredSection("Discount")
            .Get<DiscountSettings>();

        var services = new ServiceCollection()
            .AddSingleton(configuration.GetRequiredSection("Discount").Get<DiscountSettings>()!)
            .Configure<DiscountSettings>(configuration.GetRequiredSection("Discount")) // Options Pattern
            .AddSingleton<IValidateOptions<DiscountSettings>, DiscountSettingsValidator>()
            .BuildServiceProvider();

        var settings = services.GetRequiredService<DiscountSettings>();
        var options = services.GetRequiredService<IOptions<DiscountSettings>>().Value;
        var optionsSnapshot = services.GetRequiredService<IOptionsSnapshot<DiscountSettings>>().Value;
        var optionsMonitor = services.GetRequiredService<IOptionsMonitor<DiscountSettings>>().CurrentValue;
        
        Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration(c => c.AddJsonFile("more-appsettings.json"))
            .Build();

        Console.WriteLine($"Food Discount Enabled: {settings.Food.Enabled}");
        Console.WriteLine($"Options - Food Discount Enabled: {options.Food.Enabled}");
        Console.WriteLine($"Options Snapshot - Food Discount Enabled: {optionsSnapshot.Food.Enabled}");
        Console.WriteLine($"Options Monitor - Food Discount Enabled: {optionsMonitor.Food.Enabled}");
        
        Console.WriteLine($"Food Discount Percent: {settings.Food.Percent}");
        Console.WriteLine($"Options - Food Discount Percent: {options.Food.Percent}");
        Console.WriteLine($"Options Snapshot - Food Discount Percent: {optionsSnapshot.Food.Percent}");
        Console.WriteLine($"Options Monitor - Food Discount Percent: {optionsMonitor.Food.Percent}");
    }
}