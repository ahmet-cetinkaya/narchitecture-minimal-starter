using NArchitecture.Core.Mailing.MailKit.Models;

namespace NArchitecture.Starter.WebApi.Shared.Configurations;

public static class MailConfigurationExtensions
{
    /// <summary>
    /// Gets the mail configuration from the configuration.
    /// </summary>
    public static MailConfigration GetMailConfiguration(this IConfiguration configuration)
    {
        MailConfigration mailConfiguration =
            configuration.GetSection("Mail").Get<MailConfigration>()
            ?? throw new InvalidOperationException("Mail configuration is not found.");
        return mailConfiguration;
    }
}
