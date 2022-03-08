namespace EventSourcing.Readonly;

public class ViewEntity
{
    public Guid Id { get; set; }
    public long Serial { get; set; }
}