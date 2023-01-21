using Bot.Domain.Common;

namespace Bot.Domain.Entities;

public class Event : BaseAuditiableEntity
{
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public int FkReward { get; set; }
    public DateTime DateStart { get; set; }
    public DateTime ExpireAt { get; set; }

    private bool _done;

    public bool IsCompleted { get => _done; set 
        {
            if(value == true && _done == false)
            {
                // AddDomainEvent(new EventCompletedEvent(this);
            }

            _done = true;
        } 
    }


}