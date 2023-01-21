using Bot.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bot.Infrastructure.Persistence.Configurations;

public class EventUserConfiguration : IEntityTypeConfiguration<EventUser>
{
    public void Configure(EntityTypeBuilder<EventUser> builder)
    {
        builder.ToTable("event_user");
        builder.HasKey(prop => prop.Id);

        builder.Property(prop => prop.Id)
            .HasColumnName("id");

        builder.Property(prop => prop.FkUser)
            .HasColumnName("fk_user_id");

        builder.Property(prop => prop.FkEvent)
            .HasColumnName("fk_event");
    }
}
