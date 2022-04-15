using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Orders.Order;
using Orders.User;

namespace Orders;

public class DatabaseContext : DbContext
{
    public DbSet<OrderEntity> Orders => Set<OrderEntity>();
    public DbSet<UserEntity> Users => Set<UserEntity>();

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
     : base(options)
    {
    }
}