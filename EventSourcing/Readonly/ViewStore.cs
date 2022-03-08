using EventSourcing.Legacy;

namespace EventSourcing.Readonly;

public class ViewStore : IAsyncDisposable
{
    private readonly Context _context = new();

    public IQueryable<T> Set<T>() where T : class => _context.Set<T>().AsQueryable();

    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
    }
}