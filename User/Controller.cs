using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Orders.User;

public class UserController : ControllerBase
{
    private readonly DatabaseContext _context;

    public UserController(DatabaseContext context)
    {
        _context = context;
    }

    /// <summary>Register a new user.</summary>
    /// <param name="registerUserDto">User registration info.</param>
    /// <response code="200">Newly registered user.</response>
    /// <response code="409">Failed to register a user: username already taken.</response>
    [HttpPost]
    [ProducesResponseType(typeof(ReadUserDto),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> RegisterNewUser([FromBody] RegisterUserDto registerUserDto)
    {
        var usernameTaken = await _context.Users.AnyAsync(user => user.Username == registerUserDto.Username);
        if (usernameTaken)
            return Conflict("Username already taken.");
        
        var newUser = new UserEntity
        {
            Id = Guid.NewGuid(),
            DisplayName = registerUserDto.DisplayName,
            Username = registerUserDto.Username,
            Password = BCrypt.Net.BCrypt.HashPassword(registerUserDto.Password)
        };
        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        var readUserDto = new ReadUserDto
        {
            Id = newUser.Id,
            DisplayName = newUser.DisplayName,
            Username = newUser.Username
        };
        
        return Ok(readUserDto);
    }
}