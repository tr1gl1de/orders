using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Orders.Order;

namespace Orders;

public class DatabaseContext : DbContext
{
    public DbSet<OrderEntity> Orders => Set<OrderEntity>();

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
     : base(options)
    {
    }
}