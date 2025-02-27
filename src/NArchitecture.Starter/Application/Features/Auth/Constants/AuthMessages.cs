namespace NArchitecture.Starter.Application.Features.Auth.Constants;

/// <summary>
/// Authentication related message constants using dots for hierarchical organization
/// </summary>
public static class AuthMessages
{
    public static class User
    {
        public const string NotFound = "user.not_found";
        public const string InvalidCredentials = "user.invalid_credentials";
    }

    public static class Authenticator
    {
        public const string NotFound = "authenticator.not_found";
        public const string InvalidCode = "authenticator.invalid_code";
        public const string CodeRequired = "authenticator.code_required";
        public const string CodeMustBeSixDigits = "authenticator.code_must_be_six_digits";
        public const string CodeOnlyDigits = "authenticator.code_must_contain_only_digits";
    }

    public static class Validation
    {
        public const string EmailRequired = "validation.email_required";
        public const string ValidEmailRequired = "validation.valid_email_required";
        public const string PasswordRequired = "validation.password_required";
        public const string PasswordMinLength = "validation.password_min_length";
        public const string IpAddressRequired = "validation.ip_address_required";
    }
}
