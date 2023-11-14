using IConfigurationDemo.Common.Models;
using Microsoft.Extensions.Options;

namespace IConfigurationDemo.Common;

public class DiscountSettingsValidator : IValidateOptions<DiscountSettings>
{
    public ValidateOptionsResult Validate(string? name, DiscountSettings options)
    {
        // New in .NET 8.0.
        // Use ValidateOptionsResult.Fail() and ValidateOptionsResult.Success in earlier versions. 
        var resultBuilder = new ValidateOptionsResultBuilder();

        if (options.Food.Percent is < 0 or > 100)
        {
            resultBuilder.AddError($"{options.Food.Percent} doesn't match range 0 - 100.");
        }

        return resultBuilder.Build();
    }
}