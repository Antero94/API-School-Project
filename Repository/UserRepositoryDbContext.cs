using Microsoft.EntityFrameworkCore;
using StudentBloggAPI.Data;
using StudentBloggAPI.Models.Entities;
using StudentBloggAPI.Repository.Interfaces;

namespace StudentBloggAPI.Repository;

public class UserRepositoryDbContext : IUserRepo
{
    private readonly StudentBloggDbContext _dbContext;
    public UserRepositoryDbContext(StudentBloggDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> RegisterAsync(User user)
    {
        var userToAdd = await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();

        if (userToAdd != null)
        {
            return userToAdd.Entity;
        }
        return null;
    }

    public async Task<User?> DeleteUserByIdAsync(int Id)
    {
        var userToDelete = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == Id);

        if (userToDelete != null)
        {
            var entity = _dbContext.Users.Remove(userToDelete);
            await _dbContext.SaveChangesAsync();
            return entity.Entity;
        }
        return null;
    }

    public async Task<User?> GetUserByIdAsync(int Id)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == Id);
    }

    public async Task<ICollection<User>> GetPageAsync(int pageNr, int pageSize)
    {
        return await _dbContext.Users
            .OrderBy(x => x.Id)
            .Skip((pageNr - 1) * pageSize)
            .Take(pageSize).ToListAsync();
    }

    public async Task<User?> UpdateUserAsync(int Id, User user)
    {
        await _dbContext.Users.Where(u => u.Id == Id)
            .ExecuteUpdateAsync(setters => setters
            .SetProperty(x => x.Id, user.Id)
            .SetProperty(x => x.UserName, user.UserName)
            .SetProperty(x => x.FirstName, user.FirstName)
            .SetProperty(x => x.LastName, user.LastName)
            .SetProperty(x => x.Email, user.Email)
            .SetProperty(x => x.Updated, DateTime.Now));
        await _dbContext.SaveChangesAsync();

        var userToUpdate = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == Id);
        if (userToUpdate == null) return null;

        return userToUpdate;
    }

    public async Task<User?> GetByUsernameAsync(string userName)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName == userName);
        
        return user;
    }
}
