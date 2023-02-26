namespace Bot.Domain.Events;

public class QuizCreatedEvent : BaseEvent
{
    public QuizCreatedEvent(Quiz quiz)
    {
        Item = quiz;
    }

    public Quiz Item { get; set; }
}
