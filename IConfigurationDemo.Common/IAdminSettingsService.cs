using IConfigurationDemo.Common.Models;

namespace IConfigurationDemo.Common;

public interface IAdminSettingsService
{
    Task<DiscountSettings> GetAsync();

    Task<bool> IsFoodDiscountEnabledAsync();
    
    Task<decimal> GetFoodDiscountPercentAsync();
}