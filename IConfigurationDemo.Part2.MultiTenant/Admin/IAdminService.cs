namespace IConfigurationDemo.Part2.MultiTenant.Admin;

public interface IAdminService
{
    Task<IDictionary<string, IDictionary<string, string>>> GetSettingsAsync();
}