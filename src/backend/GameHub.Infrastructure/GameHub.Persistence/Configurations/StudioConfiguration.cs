using GameHub.Domain.Entities;
using GameHub.Domain.ValueObjects.Studio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameHub.Persistence.Configurations;

public sealed class StudioConfiguration : IEntityTypeConfiguration<Studio>
{
    public void Configure(EntityTypeBuilder<Studio> builder)
    {
        builder.ToTable("Studios");

        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id)
            .HasConversion(id => id.Value, value => new StudioId(value))
            .ValueGeneratedNever();

        builder.Property(s => s.Name)
            .HasConversion(n => n.Value, value => new Name(value))
            .HasMaxLength(200)
            .IsRequired();
    }
}
