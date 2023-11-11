namespace IConfigurationDemo.Part2.Admin;

public interface IAdminService
{
    Task<IDictionary<string, string>> GetSettingsAsync();
}