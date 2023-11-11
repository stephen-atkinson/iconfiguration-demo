using IConfigurationDemo_Part2;
using IConfigurationDemo.Common;
using IConfigurationDemo.Common.Extensions;
using IConfigurationDemo.Common.Models;
using IConfigurationDemo.Part2.MultiTenant.Admin;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace IConfigurationDemo.Part2.MultiTenant.Extensions;

public static class ConfigurationBuilderExtensions
{
    public static IConfigurationBuilder AddAdminSettings(this IConfigurationBuilder builder)
    {
        var tempConfig = builder.Build();

        var settings = tempConfig
            .GetRequiredSection(ConfigurationKeys.AdminApi)
            .GetRequired<AdminApiSettings>();

        var options = new OptionsWrapper<AdminApiSettings>(settings);

        var adminService = new ApiAdminService(options);
        
        return builder.Add(new AdminConfigurationSource(adminService));
    }
}