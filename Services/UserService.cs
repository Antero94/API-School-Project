using StudentBloggAPI.Mappers.Interfaces;
using StudentBloggAPI.Models.DTOs;
using StudentBloggAPI.Models.Entities;
using StudentBloggAPI.Repository.Interfaces;
using StudentBloggAPI.Services.Interfaces;

namespace StudentBloggAPI.Services;

public class UserService : IUserService
{
    private readonly IMapper<User, UserDTO> _userMapper;
    private readonly IMapper<User, UserRegistrationDTO> _regMapper;
    private readonly IUserRepo _userRepo;

    public UserService(IMapper<User, UserDTO> userMapper, IMapper<User, UserRegistrationDTO> regMapper, IUserRepo userRepo)
    {
        _userMapper = userMapper;
        _userRepo = userRepo;
        _regMapper = regMapper;
    }

    public async Task<UserDTO?> RegisterAsync(UserRegistrationDTO userRegDTO)
    {
        var user = _regMapper.MapToModel(userRegDTO);
        user.Salt = BCrypt.Net.BCrypt.GenerateSalt();
        user.HashedPassword = BCrypt.Net.BCrypt.HashPassword(userRegDTO.Password, user.Salt);

        var res = await _userRepo.RegisterAsync(user);

        return _userMapper.MapToDTO(res!);
    }

    public async Task<UserDTO?> DeleteUserByIdAsync(int Id)
    {
        var userToDelete = await _userRepo.GetUserByIdAsync(Id);
        if (userToDelete == null)
        {
            return null;
        }

        var res = await _userRepo.DeleteUserByIdAsync(Id);
        if (res == null)
        {
            return null;
        }
        return _userMapper.MapToDTO(userToDelete);
    }

    public async Task<UserDTO?> GetUserByIdAsync(int Id)
    {
        var user = await _userRepo.GetUserByIdAsync(Id);
        if (user == null)
        {
            return null;
        }

        return _userMapper.MapToDTO(user);
    }

    public async Task<UserDTO?> GetByUsernameAsync(string userName)
    {
        var user = await _userRepo.GetByUsernameAsync(userName);
        if (user == null)
        {
            return null;
        }
        return _userMapper.MapToDTO(user);
    }

    public async Task<ICollection<UserDTO>> GetPageAsync(int pageNr, int pageSize)
    {
        var users = await _userRepo.GetPageAsync(pageNr, pageSize);
        var dto = users.Select(_userMapper.MapToDTO).ToList();

        return dto;
    }

    public async Task<int> IsUserAuthorizedAsync(string userName, string password)
    {
        var user = await _userRepo.GetByUsernameAsync(userName);
        if (user == null) return 0;

        if (BCrypt.Net.BCrypt.Verify(password, user.HashedPassword))
        {
            return user.Id;
        }
        return 0;
    }

    public async Task<UserDTO?> UpdateUserAsync(int Id, UserDTO userDTO)
    {
        var userToUpdate = await _userRepo.GetUserByIdAsync(Id);
        if (userToUpdate == null)
        {
            return null;
        }
        var userUpdated = _userMapper.MapToModel(userDTO);
        userUpdated.Id = Id;

        var res = await _userRepo.UpdateUserAsync(Id, userUpdated);

        return res != null ? _userMapper.MapToDTO(userUpdated) : null;
    }
}
