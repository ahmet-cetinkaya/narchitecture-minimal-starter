using NArchitectureCoreEntities = NArchitecture.Core.Security.Abstractions.Authorization.Entities;

namespace NArchitecture.Starter.Domain.Features.Auth.Entities;

public class OperationClaim(string name) : NArchitectureCoreEntities.OperationClaim<short>(name)
{
    [Obsolete("This constructor is for ORM, mapper etc.. Do not use it in the code.", true)]
    public OperationClaim()
        : this(default!) { }

    public virtual ICollection<UserOperationClaim>? UserOperationClaims { get; set; }
    public virtual ICollection<UserGroupOperationClaim>? UserGroupOperationClaims { get; set; }
}
