using Bot.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bot.Infrastructure.Persistence.Configurations;

public class QuizConfiguration : IEntityTypeConfiguration<Quiz>
{
    public void Configure(EntityTypeBuilder<Quiz> builder)
    {
        builder.ToTable("quiz");
        builder.HasKey(prop => prop.Id);

        builder.Property(prop => prop.Id)
            .HasColumnName("id");

        builder.Property(prop => prop.Answer)
            .HasColumnName("answer");

        builder.Property(prop => prop.Question)
            .HasColumnName("question");

        builder.Property(prop => prop.Title)
            .HasColumnName("title");

        builder.Property(prop => prop.Hint)
            .HasColumnName("hint");

        builder.Property(prop => prop.FkEvent)
            .HasColumnName("fk_event");

        builder.Property(prop => prop.HasNextQuestion)
            .HasColumnName("has_next_question");

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
