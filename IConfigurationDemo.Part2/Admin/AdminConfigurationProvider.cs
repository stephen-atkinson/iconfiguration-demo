using Microsoft.Extensions.Configuration;

namespace IConfigurationDemo.Part2.Admin;

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

        foreach (var setting in settings)
        {
            var key = setting.Key.Replace(KeyDelimiter, ConfigurationPath.KeyDelimiter);
            data[key] = setting.Value;
        }

        Data = data;
    }
}