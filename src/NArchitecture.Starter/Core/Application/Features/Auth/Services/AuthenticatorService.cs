using NArchitecture.Core.Mailing.Abstractions;
using NArchitecture.Core.Security.Abstractions.Authenticator;
using NArchitecture.Core.Security.Abstractions.Authenticator.Otp;
using NArchitecture.Core.Security.Abstractions.Cryptography.Generation;
using NArchitecture.Core.Sms.Abstractions;
using NArchitecture.Starter.Application.Features.Auth.Services.Abstractions;
using NArchitectureCoreSecurity = NArchitecture.Core.Security.Authenticator;

namespace NArchitecture.Starter.Application.Features.Auth.Services;

public class AuthenticatorService(
    IUserAuthenticatorRepository userAuthenticatorRepository,
    ICodeGenerator codeGenerator,
    IAuthenticatorConfiguration authenticatorConfiguration,
    IMailService? mailService = null,
    ISmsService? smsService = null,
    IOtpService? otpService = null
)
    : NArchitectureCoreSecurity.AuthenticatorService<short, Guid, Guid, Guid, Guid, Guid, Guid>(
        userAuthenticatorRepository,
        codeGenerator,
        authenticatorConfiguration,
        mailService,
        smsService,
        otpService
    ),
        IAuthenticatorService;
