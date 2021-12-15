using Emeraude.Infrastructure.Identity.Services;
using Emeraude.Infrastructure.Persistence.Context;
using EmStartTemplate.Application.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EmStartTemplate.Infrastructure.Persistence;

public class EntityContext : EmContext<EntityContext>, IEntityContext
{
    public EntityContext(
        DbContextOptions<EntityContext> options,
        ICurrentUser currentUser)
        : base(options, currentUser)
    {
    }
}