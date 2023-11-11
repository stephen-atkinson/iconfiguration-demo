using Microsoft.Extensions.Configuration;

namespace IConfigurationDemo.Part2.MultiTenant.Admin;

public class AdminConfigurationSource : IConfigurationSource
{
    private readonly IAdminService _adminService;

    public AdminConfigurationSource(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public IConfigurationProvider Build(IConfigurationBuilder builder) => new AdminConfigurationProvider(_adminService);
}


