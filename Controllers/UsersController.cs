using Microsoft.AspNetCore.Mvc;
using StudentBloggAPI.Models.DTOs;
using StudentBloggAPI.Services.Interfaces;

namespace StudentBloggAPI.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IPostService _postService;
    public UsersController(IUserService userService, IPostService postService)
    {
        _userService = userService;
        _postService = postService;
    }

    [HttpGet(Name = "GetAllUsers")]
    public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsersAsync(int pageNr = 1, int pageSize = 10)
    {
        return Ok(await _userService.GetPageAsync(pageNr, pageSize));
    }

    [HttpGet("{userId:int}", Name = "GetUserById")]
    public async Task<ActionResult<UserDTO>> GetUserByIdAsync(int userId)
    {
        var user = await _userService.GetUserByIdAsync(userId);
        if (user == null)
        {
            return NotFound("Fant ikke bruker");
        }
        return Ok(user);
    }

    [HttpGet("{Username}", Name = "GetUserByUsername")]
    public async Task<ActionResult<UserDTO>> GetUserByUsernameAsync(string Username)
    {
        var user = await _userService.GetByUsernameAsync(Username);
        if (user == null)
        {
            return NotFound("Fant ikke bruker");
        }
        return Ok(user);
    }

    [HttpGet("{userId:int}/posts", Name = "GetUserPosts")]
    public async Task<ActionResult<PostDTO>> GetUserPostsAsync(int userId, int pageNr = 1, int pageSize = 10)
    {
        var userPosts = await _postService.GetAllPostsAsync(pageNr, pageSize);
        var dto = userPosts.Where(x => x.UserId == userId).ToList();

        if (dto.Count == 0)
        {
            return NotFound("Fant ingen poster");
        }
        return Ok(dto);
    }

    [HttpPost("Register", Name = "RegisterUser")]
    public async Task<ActionResult<UserDTO>> RegisterUserAsync(UserRegistrationDTO userRegDTO)
    {
        if (!ModelState.IsValid) { return BadRequest(ModelState); }

        var userToAdd = await _userService.RegisterAsync(userRegDTO);
        if (userToAdd == null)
        {
            return BadRequest("Kunne ikke legge til bruker");
        }
        return Ok(userToAdd);
    }

    [HttpDelete("{userId}", Name = "DeleteUser")]
    public async Task<ActionResult<UserDTO>> DeleteUserAsync(int userId)
    {
        if (!ModelState.IsValid) { return BadRequest(ModelState); }

        var userToDelete = await _userService.DeleteUserByIdAsync(userId);
        if (userToDelete == null)
        {
            return NotFound("Fant ikke bruker");
        }
        return Ok(userToDelete);
    }

    [HttpPut("{userId}", Name = "UpdateUser")]
    public async Task<ActionResult<UserDTO>> UpdateUserAsync(int userId, UserDTO user)
    {
        if (!ModelState.IsValid) { return BadRequest(ModelState); }

        var userToUpdate = await _userService.UpdateUserAsync(userId, user);
        if (userToUpdate == null)
        {
            return NotFound("Fant ikke bruker");
        }
        return Ok(userToUpdate);
    }
}
