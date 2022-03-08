using Microsoft.EntityFrameworkCore;

namespace EventSourcing.Events;

public class EventContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=db2.db");
    }
}