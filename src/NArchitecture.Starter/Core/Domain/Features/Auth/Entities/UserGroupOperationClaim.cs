using NArchitectureCoreEntities = NArchitecture.Core.Security.Abstractions.Authorization.Entities;

namespace NArchitecture.Starter.Domain.Features.Auth.Entities;

public class UserGroupOperationClaim(Guid userGroupId, short operationClaimId)
    : NArchitectureCoreEntities.UserGroupOperationClaim<Guid, short, Guid, Guid, Guid, Guid, Guid>(userGroupId, operationClaimId)
{
    [Obsolete("This constructor is for ORM, mapper etc.. Do not use it in the code.", true)]
    public UserGroupOperationClaim()
        : this(default, default) { }

    public virtual UserGroup? UserGroup { get; set; }
    public virtual OperationClaim? OperationClaim { get; set; }
}
