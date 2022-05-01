using Microsoft.EntityFrameworkCore;
using Orders.User;

namespace Orders.Services;

class UserService : IUserService
{
    private readonly DatabaseContext context;

    public UserService(DatabaseContext context)
    {
        this.context = context;
    }
    
    public async Task<IEnumerable<UserEntity>> GetAllAsync()
    {
        return await context.Users.ToListAsync();
    }

    public async Task<UserEntity?> GetByIdAsync(Guid id)
    {
        return await context.Users.SingleAsync(user => user.Id == id);
    }

    public async Task<UserEntity?> GetByNameAsync(string name)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.Username == name);
    }

    public async Task<bool> IsExist(string name)
    {
        return await context.Users.AnyAsync(user => user.Username == name);
    }

    public void Create(UserEntity user)
    {
        context.Users.Add(user);
    }

    public async Task SaveAsync()
    {
         await context.SaveChangesAsync();
    }
}