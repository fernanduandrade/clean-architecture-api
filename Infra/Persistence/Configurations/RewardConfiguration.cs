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
            .HasColumnName("badge");

        builder.Property(prop => prop.Earned)
            .HasColumnName("earned");

        builder.Property(prop => prop.ParticipantReward)
            .HasColumnName("participant_reward");

        builder.Property(prop => prop.Coin)
            .HasColumnName("he4rt_coin");

        builder.Property(prop => prop.Xp)
            .HasColumnName("he4rt_xp");
    }
}
