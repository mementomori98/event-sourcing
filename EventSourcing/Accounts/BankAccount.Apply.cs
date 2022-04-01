using EventSourcing.Abstractions;

namespace EventSourcing.Accounts;

public partial class BankAccount : AggregateRoot
{
    private void Apply(AccountOpened e)
    {
        Id = e.Id;
        State = States.Opened;
    }

    private void Apply(MoneyTransferred e)
    {
        Balance -= e.Amount;
    }

    private void Apply(AccountClosed e)
    {
        State = States.Closed;
    }

    protected void Append(Event e)
    {
        ((dynamic) this).Apply((dynamic) e);
        UncommittedEvents.Add(e);
    }


    public override void Replay(List<Event?> events)
    {
        foreach (var e in events)
            ((dynamic) this).Apply((dynamic) e);
    }
}