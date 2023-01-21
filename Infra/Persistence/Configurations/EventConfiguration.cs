using Bot.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bot.Infrastructure.Persistence.Configurations;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.ToTable("event");
        builder.HasKey(prop => prop.Id);

        builder.Property(prop => prop.Id)
            .HasColumnName("id");

        builder.Property(prop => prop.Description)
            .HasColumnName("description");

        builder.Property(prop => prop.DateStart)
            .HasColumnName("date_start");

        builder.Property(prop => prop.ExpireAt)
            .HasColumnName("expire_at");

        builder.Property(prop => prop.FkReward)
            .HasColumnName("fk_reward");

        builder.Property(prop => prop.IsActive)
            .HasColumnName("is_active");
    }
}
