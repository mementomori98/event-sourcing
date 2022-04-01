

using EventSourcing;
using EventSourcing.Abstractions;
using EventSourcing.Accounts;

Console.WriteLine("Hello World");

await InitAsync();

await using var x = new BankAccountStore();
await using var y = new BankAccountStore();
IStore<BankAccount> store = x;

var account = new BankAccount();
account.Open();
account.Transfer(-500, "");
account.Transfer(200, "");
var id = account.Id;

await store.SaveAsync(account);

async Task InitAsync()
{
    await using var store = new BankAccountStore();
    await store.Database.EnsureDeletedAsync();
    await store.Database.EnsureCreatedAsync();
}
