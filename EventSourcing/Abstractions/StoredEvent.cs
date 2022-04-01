namespace EventSourcing.Abstractions;

public record StoredEvent(Guid AggregateId, long Serial, string Data);