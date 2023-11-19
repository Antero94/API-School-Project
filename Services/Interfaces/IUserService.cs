using StudentBloggAPI.Models.DTOs;

namespace StudentBloggAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<ICollection<UserDTO>> GetPageAsync(int pageNr, int pageSize);
        Task<UserDTO?> GetByUsernameAsync(string userName);
        Task<UserDTO?> RegisterAsync(UserRegistrationDTO userDTO);
        Task<UserDTO?> UpdateUserAsync(int Id, UserDTO userDTO);
        Task<UserDTO?> DeleteUserByIdAsync(int Id);
        Task<UserDTO?> GetUserByIdAsync(int Id);
        Task<int> IsUserAuthorizedAsync(string userName, string password);
    }
}
