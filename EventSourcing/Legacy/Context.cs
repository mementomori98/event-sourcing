using EventSourcing.Readonly;
using Microsoft.EntityFrameworkCore;

namespace EventSourcing.Legacy;

public class Context : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=db.db");
    }

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<Tenant>().HasKey(t => t.Id);
        
        b.Entity<Insurance>().HasKey(i => i.Id);
        b.Entity<Insurance>().HasOne<Tenant>().WithMany().HasForeignKey(i => i.TenantId);
        
        b.Entity<InsuranceLine>().HasKey(l => l.Id);
        b.Entity<InsuranceLine>().HasOne<Insurance>().WithMany().HasForeignKey(l => l.InsuranceId);
    }
}