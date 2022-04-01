namespace EventSourcing.Abstractions;

public abstract class AggregateRoot
{
    protected ICollection<Event> UncommittedEvents = new List<Event>();

    public Guid Id { get; protected set; }

    public ICollection<Event> Commit()
    {
        var events = UncommittedEvents;
        UncommittedEvents = new List<Event>();
        return events;
    }

    public abstract void Replay(List<Event?> events);
}