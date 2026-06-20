using GameHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameHub.Persistence;

public sealed class GameHubDbContext : DbContext
{
    public const string CreatedAt = "CreatedAt";
    public const string ModifiedAt = "ModifiedAt";

    private readonly TimeProvider _clock;

    public GameHubDbContext(DbContextOptions<GameHubDbContext> options, TimeProvider clock)
        : base(options)
    {
        _clock = clock;
    }

    public DbSet<Game> Games => Set<Game>();
    public DbSet<Studio> Studios => Set<Studio>();
    public DbSet<Platform> Platforms => Set<Platform>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GameHubDbContext).Assembly);

        // Audit columns live only in the persistence model (shadow properties),
        // never on the domain entities. Applied to aggregate roots, not owned types.
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (entityType.IsOwned())
                continue;

            modelBuilder.Entity(entityType.ClrType).Property<DateTimeOffset>(CreatedAt);
            modelBuilder.Entity(entityType.ClrType).Property<DateTimeOffset?>(ModifiedAt);
        }

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyAudit();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        ApplyAudit();
        return base.SaveChanges();
    }

    private void ApplyAudit()
    {
        var now = _clock.GetUtcNow();

        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Metadata.FindProperty(CreatedAt) is null)
                continue;

            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Property(CreatedAt).CurrentValue = now;
                    break;

                case EntityState.Modified:
                    entry.Property(ModifiedAt).CurrentValue = now;
                    entry.Property(CreatedAt).IsModified = false;
                    break;
            }
        }
    }
}
