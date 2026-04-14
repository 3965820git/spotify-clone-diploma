using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpotifyClone.Shared.BuildingBlocks.Application.Outbox;

namespace SpotifyClone.Accounts.Infrastructure.Persistence.Accounts.Configurations;

internal sealed class OutboxMessageEfCoreConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("outbox_messages");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();

        builder.Property(x => x.Type)
            .HasColumnName("type")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.Content)
            .HasColumnName("content")
            .IsRequired();

        builder.Property(x => x.OccurredOn)
            .HasColumnName("occured_on")
            .IsRequired();

        builder.Property(x => x.ProcessedOn)
            .HasColumnName("processed_on");

        builder.Property(x => x.Error)
            .HasColumnName("error");

        builder.HasIndex(x => x.ProcessedOn);
    }
}
