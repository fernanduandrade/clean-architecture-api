using Bot.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bot.Infrastructure.Persistence.Configurations;

public class RewardConfiguration : IEntityTypeConfiguration<Reward>
{
    public void Configure(EntityTypeBuilder<Reward> builder)
    {
        builder.ToTable("reward");
        builder.HasKey(prop => prop.Id);

        builder.Property(prop => prop.Id)
            .HasColumnName("id");

        builder.Property(prop => prop.Role)
            .HasColumnName("role");

        builder.Property(prop => prop.Claimed)
            .HasColumnName("claimed");

        builder.Property(prop => prop.ParticipantReward)
            .HasColumnName("participant_reward");

        builder.Property(prop => prop.Coin)
            .HasColumnName("coins");

        builder.Property(prop => prop.Expirience)
            .HasColumnName("expirience");

        builder.Property(prop => prop.FkEvent)
            .HasColumnName("fk_event");

        builder.Property(prop => prop.CreatedBy)
            .HasColumnName("created_by");

        builder.Property(prop => prop.Created)
            .HasColumnName("created");

        builder.Property(prop => prop.LastModified)
            .HasColumnName("last_modified");

        builder.Property(prop => prop.LastModifiedBy)
            .HasColumnName("last_modified_by");
    }
}
