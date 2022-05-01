using Orders.User;

namespace Orders.Services;

public interface IUserService
{
    public Task<IEnumerable<UserEntity>> GetAllAsync();
    public Task<UserEntity?> GetByIdAsync(Guid id);
    public Task<UserEntity?> GetByNameAsync(string name);
    public Task<bool> IsExist(string name);
    public void Create(UserEntity user);
    public Task SaveAsync();

}