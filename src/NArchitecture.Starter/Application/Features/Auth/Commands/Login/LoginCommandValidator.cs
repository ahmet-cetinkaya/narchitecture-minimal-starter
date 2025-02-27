using FluentValidation;
using NArchitecture.Starter.Application.Features.Auth.Constants;
using NArchitectureCoreValidation = NArchitecture.Core.Validation.Abstractions;

namespace NArchitecture.Starter.Application.Features.Auth.Commands.Login;

/// <summary>
/// Validator for LoginCommand
/// </summary>
public class LoginCommandValidator : AbstractValidator<LoginCommand>, NArchitectureCoreValidation.IValidationProfile<LoginCommand>
{
    public LoginCommandValidator()
    {
        _ = RuleFor(c => c.Email)
            .NotEmpty()
            .WithMessage(AuthMessages.Validation.EmailRequired)
            .EmailAddress()
            .WithMessage(AuthMessages.Validation.ValidEmailRequired);

        _ = RuleFor(c => c.Password)
            .NotEmpty()
            .WithMessage(AuthMessages.Validation.PasswordRequired)
            .MinimumLength(6)
            .WithMessage(AuthMessages.Validation.PasswordMinLength);

        _ = RuleFor(c => c.IpAddress).NotEmpty().WithMessage(AuthMessages.Validation.IpAddressRequired);

        // AuthenticatorCode can be null, but if provided it should be valid
        _ = When(
            c => c.AuthenticatorCode != null,
            () =>
            {
                _ = RuleFor(c => c.AuthenticatorCode)
                    .Length(6)
                    .WithMessage(AuthMessages.Authenticator.CodeMustBeSixDigits)
                    .Matches("^[0-9]+$")
                    .WithMessage(AuthMessages.Authenticator.CodeOnlyDigits);
            }
        );
    }
}
