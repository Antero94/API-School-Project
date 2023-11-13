using StudentBloggAPI.Mappers.Interfaces;
using StudentBloggAPI.Models.DTOs;
using StudentBloggAPI.Models.Entities;

namespace StudentBloggAPI.Mappers;

public class UserRegMapper : IMapper<User, UserRegistrationDTO>
{
    public UserRegistrationDTO MapToDTO(User model)
    {
        throw new NotImplementedException();
    }

    public User MapToModel(UserRegistrationDTO dto)
    {
        var dtNow = DateTime.Now;
        return new User()
        {
            UserName = dto.UserName,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            IsAdminUser = false,
            Created = dtNow,
            Updated = dtNow
        };
    }
}
