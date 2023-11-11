using IConfigurationDemo_Part2;
using Microsoft.Extensions.Configuration;

namespace IConfigurationDemo.Part2.MultiTenant.Admin;

public class AdminConfigurationProvider : ConfigurationProvider
{
    private const string KeyDelimiter = "_";
    
    private readonly IAdminService _adminService;

    public AdminConfigurationProvider(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public override void Load() => LoadAsync().GetAwaiter().GetResult();

    private async Task LoadAsync()
    {
        var settings = await _adminService.GetSettingsAsync();

        var data = new Dictionary<string, string?>();

        foreach (var tenant in settings)
        {
            foreach (var tenantSettings in tenant.Value)
            {
                // Tenant:LeedsCafe:Discount:Food:Enabled
                var keyPrefix = ConfigurationKeys.Tenant + ConfigurationPath.KeyDelimiter + tenant.Key + ConfigurationPath.KeyDelimiter;
                var key = keyPrefix + tenantSettings.Key.Replace(KeyDelimiter, ConfigurationPath.KeyDelimiter);
                data[key] = tenantSettings.Value;
            }
        }

        Data = data;
    }
}