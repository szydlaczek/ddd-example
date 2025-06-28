using System.Runtime.CompilerServices;
using Project1.Domain.Entities;

namespace Project1.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Product> Products { get; }

    DbSet<Order> Orders { get; }

    Task<int> SaveChangesAsync(CancellationToken ct);
}
