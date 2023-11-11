using System.Text.Json;
using IConfigurationDemo.Common.Models;
using Microsoft.Extensions.Options;

namespace IConfigurationDemo.Part2.MultiTenant.Admin;

public class ApiAdminService : IAdminService, IDisposable
{
    private readonly IOptions<AdminApiSettings> _options;
    private readonly HttpClient _httpClient;

    public ApiAdminService(IOptions<AdminApiSettings> options)
    {
        _options = options;
        _httpClient = new HttpClient();
    }

    public async Task<IDictionary<string, IDictionary<string, string>>> GetSettingsAsync()
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, _options.Value.Url);
        
        using var response = await _httpClient.SendAsync(requestMessage);

        var json = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<IDictionary<string, IDictionary<string, string>>>(json) 
               ?? throw new JsonException($"Couldn't deserialise \"{json}\".");
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }
}