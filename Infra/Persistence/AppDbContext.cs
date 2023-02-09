using Bot.Application.Common.Interfaces;
using Bot.Infrastructure.Identity;
using Bot.Infrastructure.Persistence.Interceptors;
using Duende.IdentityServer.EntityFramework.Options;
using MediatR;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;
using Entities = Bot.Domain.Entities;

namespace Bot.Infrastructure.Persistence;

public class AppDbContext : ApiAuthorizationDbContext<ApplicationUser>, IAppContext
{
    private readonly IMediator _mediator;
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    public AppDbContext(
        DbContextOptions<AppDbContext> options,
        IOptions<OperationalStoreOptions> operationalStoreOptions,
        IMediator mediator,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor) : base(options, operationalStoreOptions)
    {
        _mediator= mediator;
        _auditableEntitySaveChangesInterceptor= auditableEntitySaveChangesInterceptor;
    }

    public DbSet<Entities.Quiz> Quizes => Set<Entities.Quiz>();

    public DbSet<Entities.Event> Events => Set<Entities.Event>();

    public DbSet<Entities.EventUser> EventUsers => Set<Entities.EventUser>();

    public DbSet<Entities.Reward> Rewards => Set<Entities.Reward>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEvents(this);
        var result = await base.SaveChangesAsync(cancellationToken);
        return result;
    }
}
