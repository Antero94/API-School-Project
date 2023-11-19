using StudentBloggAPI.Models.Entities;
using StudentBloggAPI.Repository.Interfaces;

namespace StudentBloggAPI.Repository.InMemoryDb;

public class UserRepoInMemory : IUserRepo
{
    private int _lastId = 0;
    private readonly List<User> _users = new List<User>();
    public UserRepoInMemory()
    {
        _lastId++;
        _users.Add(new User()
        {
            Created = DateTime.Now,
            Email = "Haugland.Jobb@gmail.com",
            FirstName = "Stian",
            LastName = "Haugland",
            Id = _lastId,
            UserName = "Haugsti",
            HashedPassword = "123"
        });
    }

    public async Task<User?> DeleteUserByIdAsync(int Id)
    {
        await Task.Delay(10);
        var userToDelete = _users.FirstOrDefault(x => x.Id == Id);
        if (userToDelete != null)
        {
            _users.Remove(userToDelete);
        }
        return null;
    }

    public async Task<User?> GetUserByIdAsync(int Id)
    {
        await Task.Delay(10);
        var user = _users.FirstOrDefault(users => users.Id == Id);
        if (user != null)
        {
            return user;
        }
        return null;
    }

    public async Task<User?> UpdateUserAsync(int Id, User user)
    {
        await Task.Delay(10);
        var userToUpdate = _users.FirstOrDefault(u => u.Id == Id);
        if (userToUpdate != null && user != null)
        {
            userToUpdate.UserName = user.UserName;
            userToUpdate.FirstName = user.FirstName;
            userToUpdate.LastName = user.LastName;
            userToUpdate.Email = user.Email;
            return user;
        }
        return null;
    }

    public Task<ICollection<User>> GetPageAsync(int pageNr, int pageSize)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetByUsernameAsync(string userName)
    {
        throw new NotImplementedException();
    }

    public Task<User?> RegisterAsync(User user)
    {
        throw new NotImplementedException();
    }
}
