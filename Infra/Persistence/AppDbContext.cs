using Bot.Application.Common.Interfaces;
using Bot.Domain.Entities;
using Bot.Infrastructure.Identity;
using Bot.Infrastructure.Persistence.Interceptors;
using Duende.IdentityServer.EntityFramework.Options;
using MediatR;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;

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

    public DbSet<Quiz> Quizes => Set<Quiz>();

    public DbSet<Event> Events => Set<Event>();

    public DbSet<EventUser> EventUsers => Set<EventUser>();

    public DbSet<Reward> Rewards => Set<Reward>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _mediator.DispatchDomainEvents(this);

        return await base.SaveChangesAsync(cancellationToken);
    }
}
