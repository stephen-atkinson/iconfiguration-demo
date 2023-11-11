using IConfigurationDemo.Common.Models;
using Microsoft.Extensions.Options;

namespace IConfigurationDemo.Common;

public class DiscountSettingsValidator : IValidateOptions<DiscountSettings>
{
    public ValidateOptionsResult Validate(string? name, DiscountSettings options)
    {
        string? failureMessage = null;

        if (options.Food.Percent is < 0 or > 100)
        {
            failureMessage = $"{options.Food.Percent} doesn't match Range 0 - 100.";
        }

        return failureMessage != null ? ValidateOptionsResult.Fail(failureMessage) : ValidateOptionsResult.Success;
    }
}