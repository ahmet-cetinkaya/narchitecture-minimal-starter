using NArchitectureCoreEntities = NArchitecture.Core.Security.Abstractions.Authorization.Entities;

namespace NArchitecture.Starter.Domain.Features.Auth.Entities;

public class UserOperationClaim(Guid userId, short operationClaimId)
    : NArchitectureCoreEntities.UserOperationClaim<Guid, short, Guid, Guid, Guid, Guid, Guid, Guid>(userId, operationClaimId)
{
    [Obsolete("This constructor is for ORM, mapper etc.. Do not use it in the code.", true)]
    public UserOperationClaim()
        : this(default, default) { }

    public virtual User? User { get; set; }
    public virtual OperationClaim? OperationClaim { get; set; }
}
