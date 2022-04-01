namespace EventSourcing.Abstractions;

public interface IStore<TAggregate> where TAggregate : AggregateRoot
{
    Task SaveAsync(TAggregate root);
    Task<TAggregate> LoadAsync(Guid rootId);
}