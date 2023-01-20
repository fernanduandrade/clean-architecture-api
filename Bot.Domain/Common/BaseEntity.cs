using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bot.Domain.Common;

public abstract class BaseEntity
{
    [Key]
    public long Id { get; set; }

    [NotMapped]
    public List<IDomainEvent> DomainEvents { get; } = new();

    public void QueueDomainEvents(IDomainEvent @event)
    {
        DomainEvents.Add(@event);
    }
}
