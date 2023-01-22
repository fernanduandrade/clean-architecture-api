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

        builder.Property(prop => prop.CreateBy)
            .HasColumnName("created_by");

        builder.Property(prop => prop.Created)
            .HasColumnName("created");

        builder.Property(prop => prop.LastModified)
            .HasColumnName("last_modified");

        builder.Property(prop => prop.LastModifiedBy)
            .HasColumnName("last_modified_by");
    }
}
