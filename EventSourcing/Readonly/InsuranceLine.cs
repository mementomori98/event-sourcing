
namespace EventSourcing.Readonly;

public class InsuranceLine : ViewEntity
{
    public Guid InsuranceId { get; set; }
    public int Amount { get; set; }
}