using EventSourcing.Abstractions;

namespace EventSourcing.Accounts;

public record AccountOpened(Guid Id) : Event;
public record MoneyTransferred(decimal Amount, string Destination) : Event;
public record AccountClosed : Event;

public partial class BankAccount
{
    public States State { get; private set; } = States.Initial;
    public decimal Balance { get; private set; }

    public void Open()
    {
        if (State != States.Initial)
            throw new InvalidOperationException("State must be initial");
        Append(new AccountOpened(Guid.NewGuid()));
    }

    public void Transfer(decimal amount, string destination)
    {
        if (State != States.Opened)
            throw new InvalidOperationException("Account must be opened");
        if (Balance < amount)
            throw new InvalidOperationException("Insufficient funds");
        Append(new MoneyTransferred(amount, destination));
    }

    public void Close()
    {
        if (State != States.Opened)
            throw new InvalidOperationException("Account must be open");
        Append(new AccountClosed());
    }

    public enum States
    {
        Initial,
        Opened,
        Closed
    }
}