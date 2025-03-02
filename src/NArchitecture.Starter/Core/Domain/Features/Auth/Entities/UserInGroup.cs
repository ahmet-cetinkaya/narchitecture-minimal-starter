using NArchitectureCoreEntities = NArchitecture.Core.Security.Abstractions.Authorization.Entities;

namespace NArchitecture.Starter.Domain.Features.Auth.Entities;

public class UserInGroup(Guid userId, Guid userGroupId)
    : NArchitectureCoreEntities.UserInGroup<Guid, short, Guid, Guid, Guid, Guid, Guid, Guid>(userId, userGroupId)
{
    [Obsolete("This constructor is for ORM, mapper etc.. Do not use it in the code.", true)]
    public UserInGroup()
        : this(default, default) { }

    public virtual User? User { get; set; }
    public virtual UserGroup? UserGroup { get; set; }
}
