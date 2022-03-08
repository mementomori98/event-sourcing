
using EventSourcing.Legacy;
using EventSourcing.Readonly;
using Microsoft.EntityFrameworkCore;

await using var context = new Context();

await context.Database.EnsureDeletedAsync();
await context.Database.EnsureCreatedAsync();

await context.AddAsync(new Tenant {Name = "Mate"});
await context.SaveChangesAsync();

var tenant = await context.Set<Tenant>().SingleAsync();

await using var store = new ViewStore();
var insurance = await store.Set<Insurance>().FirstOrDefaultAsync();

Console.WriteLine(tenant.Name);
Console.WriteLine($"Insurance: {insurance != default}");