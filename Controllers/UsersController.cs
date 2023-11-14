using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentBloggAPI.Models.DTOs;
using StudentBloggAPI.Services.Interfaces;
using System.Net.Http.Headers;

namespace StudentBloggAPI.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet(Name = "GetAllUsers")]
    public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsersAsync(int pageNr = 1, int pageSize = 10)
    {
        return Ok(await _userService.GetPageAsync(pageNr, pageSize));
    }

    [HttpGet("{Id}", Name = "GetUsersId")]
    public async Task<ActionResult<UserDTO>> GetUsersByIdAsync(int Id)
    {
        var user = await _userService.GetByIdAsync(Id);
        if (user == null)
        {
            return NotFound("Fant ikke bruker");
        }
        return Ok(user);
    }

    [HttpPost("Register", Name = "RegisterUser")]
    public async Task<ActionResult<UserDTO>> RegisterUserAsync(UserRegistrationDTO userRegDTO)
    {
        if (!ModelState.IsValid) { return BadRequest(ModelState); }

        var user = await _userService.RegisterAsync(userRegDTO);
        if (user == null)
        {
            return BadRequest("Kunne ikke legge til bruker");
        }
        return Ok(user);
    }

    [HttpDelete("{Id}", Name = "DeleteUser")]
    public async Task<ActionResult<UserDTO>> DeleteUserAsync(int Id)
    {
        var userToDelete = await _userService.DeleteByIdAsync(Id);
        if (userToDelete == null)
        {
            return NotFound();
        }
        return Ok(userToDelete);
    }

    [HttpPut("{Id}", Name = "UpdateUser")]
    public async Task<ActionResult<UserDTO>> UpdateUserAsync(int Id, UserDTO user)
    {
        if (ModelState.IsValid) { return BadRequest(ModelState); }

        var userToUpdate = await _userService.UpdateUserAsync(Id, user);
        if (userToUpdate == null)
        {
            return NotFound();
        }
        return Ok(userToUpdate);
    }
}
