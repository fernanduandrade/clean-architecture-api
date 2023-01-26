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

        builder.Property(prop => prop.IsActive)
            .HasColumnName("is_active");

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
