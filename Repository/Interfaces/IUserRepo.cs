using StudentBloggAPI.Models.Entities;

namespace StudentBloggAPI.Repository.Interfaces;

public interface IUserRepo
{
    Task<User?> RegisterAsync(User user);
    Task<User?> UpdateUserAsync(int Id, User user);
    Task<User?> DeleteUserByIdAsync(int Id);
    Task<User?> GetUserByIdAsync(int Id);
    Task<User?> GetByUsernameAsync(string userName);
    Task<ICollection<User>> GetPageAsync(int pageNr, int pageSize);
}
