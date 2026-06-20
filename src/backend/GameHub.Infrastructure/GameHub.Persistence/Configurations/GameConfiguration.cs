using GameHub.Domain.Entities;
using GameHub.Domain.ValueObjects.Game;
using GameHub.Domain.ValueObjects.Platform;
using GameHub.Domain.ValueObjects.Studio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameHub.Persistence.Configurations;

public sealed class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.ToTable("Games");

        builder.HasKey(g => g.Id);
        builder.Property(g => g.Id)
            .HasConversion(id => id.Value, value => new GameId(value))
            .ValueGeneratedNever();

        builder.Property(g => g.Title)
            .HasConversion(t => t.Value, value => new Title(value))
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(g => g.StudioId)
            .HasConversion(s => s.Value, value => new StudioId(value))
            .IsRequired();

        // Supports "all games by studio".
        builder.HasIndex(g => g.StudioId);

        // PlatformIds is owned by the Game aggregate and stored in a join table.
        // This is what makes "all games for platform" an indexed lookup.
        builder.OwnsMany(g => g.PlatformIds, platform =>
        {
            platform.ToTable("GamePlatforms");
            platform.WithOwner().HasForeignKey("GameId");

            platform.Property(p => p.Value)
                .HasColumnName("PlatformId")
                .IsRequired();

            platform.HasKey("GameId", "Value");

            // Supports "all games for platform".
            platform.HasIndex(p => p.Value);
        });

        builder.Navigation(g => g.PlatformIds)
            .UsePropertyAccessMode(PropertyAccessMode.Property);
    }
}
