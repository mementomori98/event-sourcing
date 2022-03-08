namespace EventSourcing.Events;

public interface IEvent<T>
{
    void ApplyTo(T aggregate);
}