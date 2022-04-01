using EventSourcing.Abstractions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace EventSourcing.Accounts;

public class BankAccountStore : DbContext, IStore<BankAccount>
{
    public async Task SaveAsync(BankAccount root)
    {
        var latestEvent = await Events.OrderByDescending(e => e.Serial).FirstOrDefaultAsync(e => e.AggregateId == root.Id);
        var serial = latestEvent?.Serial + 1 ?? 1;
        var events = root.Commit();
        await AddRangeAsync(events.Select((e, i) => new StoredEvent(
            AggregateId: root.Id,
            Serial: serial + i,
            Data: JsonConvert.SerializeObject(e, _settings))));
        await SaveChangesAsync();
    }

    public async Task<BankAccount> LoadAsync(Guid rootId)
    {
        var events = await Events.Where(e => e.AggregateId == rootId).OrderBy(e => e.Serial).ToListAsync();
        var root = new BankAccount();
        root.Replay(events.Select(e => JsonConvert.DeserializeObject<Event>(e.Data, _settings)).ToList());
        return root;
    }

    private DbSet<StoredEvent> Events { get; set; }

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<StoredEvent>().HasKey(e => new {e.AggregateId, e.Serial});
        b.Entity<StoredEvent>().ToTable("BankAccountEvents");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder b)
    {
        b.UseSqlite("Data Source=data.db");
    }
    
    private readonly JsonSerializerSettings _settings = new() {TypeNameHandling = TypeNameHandling.All};
}