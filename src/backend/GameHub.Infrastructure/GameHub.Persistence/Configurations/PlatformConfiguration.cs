using GameHub.Domain.Entities;
using GameHub.Domain.ValueObjects.Platform;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameHub.Persistence.Configurations;

public sealed class PlatformConfiguration : IEntityTypeConfiguration<Platform>
{
    public void Configure(EntityTypeBuilder<Platform> builder)
    {
        builder.ToTable("Platforms");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasConversion(id => id.Value, value => new PlatformId(value))
            .ValueGeneratedNever();

        builder.Property(p => p.Name)
            .HasConversion(n => n.Value, value => new Name(value))
            .HasMaxLength(200)
            .IsRequired();
    }
}
