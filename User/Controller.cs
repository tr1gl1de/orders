using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orders.Helpers;
using Orders.Services;

namespace Orders.User;

[ApiController]
[Route("/users")]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly DatabaseContext _context;
    private readonly IConfiguration _configuration;
    private readonly JwtTokenHelper _jwtTokenHelper;

    public UserController(DatabaseContext context,
        IConfiguration configuration,
        JwtTokenHelper jwtTokenHelper,
        IUserService service)
    {
        _context = context;
        _configuration = configuration;
        _jwtTokenHelper = jwtTokenHelper;
        _userService = service;
    }

    [HttpGet("[action]")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetAllAsync();
        return Ok(users);
    }

    [HttpGet("who-am-i")]
    [Authorize]
    public async Task<IActionResult> GetCurrentUserInfo()
    {
        var subClaim = User.Claims.Single(claim => claim.Type == "sub");
        var currentUserId = Guid.Parse(subClaim.Value);

        var currentUser = await _userService.GetByIdAsync(currentUserId);

        var readDto = currentUser.Map();

        return Ok(readDto);
    }

    /// <summary>Register a new user.</summary>
    /// <param name="registerUserDto">User registration info.</param>
    /// <response code="200">Newly registered user.</response>
    /// <response code="409">Failed to register a user: username already taken.</response>
    [HttpPost("register")]
    [ProducesResponseType(typeof(ReadUserDto),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> RegisterNewUser([FromBody] RegisterUserDto registerUserDto)
    {
        var usernameTaken = await _userService.IsExist(registerUserDto.Username);
        if (usernameTaken)
            return Conflict("Username already taken.");

        var newUser = registerUserDto.Map();
        _userService.Create(newUser);
        await _userService.SaveAsync();

        var readUserDto = newUser.Map();
        
        return Ok(readUserDto);
    }

    /// <summary>Authenticate the user.</summary>
    /// <param name="authUserDto">User authentication info.</param>
    /// <response code="200">Authentication token pair for specified user credentials.</response>
    /// <response code="404">User with specified username does not exist.</response>
    /// <response code="409">Incorrect password was specified.</response>
    [HttpPost("authenticate")]
    [ProducesResponseType(typeof(TokenPairDto),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> AuthenticateUser([FromBody] AuthenticateUserDto authUserDto)
    {
        // var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == authUserDto.Username);
        var user = await _userService.GetByNameAsync(authUserDto.Username);
        if (user is null)
            return NotFound();
        
        if(!BCrypt.Net.BCrypt.Verify(authUserDto.Password, user.Password))
            return Conflict("Incorrect password");

        var refreshTokenLifetime = int.Parse(_configuration["JwtAuth:RefreshTokenLifetime"]);
        var refreshTokenEntity = new RefreshTokenEntity
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            ExpirationTime = DateTime.UtcNow.AddDays(refreshTokenLifetime)
        };

        _context.RefreshTokens.Add(refreshTokenEntity);
        await _userService.SaveAsync();

        var tokenPair = _jwtTokenHelper.IssuerTokenPair(user.Id, refreshTokenEntity.Id, user.Roles);
        var tokenPairDto = tokenPair.Map();
        return Ok(tokenPairDto);
    }

    /// <summary>Refresh token pair.</summary>
    /// <param name="refreshToken">Refresh token.</param>
    /// <response code="200">A new token pair.</response>
    /// <response code="400">Invalid refresh token was provided.</response>
    /// <response code="409">Provided refresh token has already been used.</response>
    [HttpPost("refresh")]
    [ProducesResponseType(typeof(TokenPairDto),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> RefreshTokenPair([FromBody] string refreshToken)
    {
        var refreshTokenClaims = _jwtTokenHelper.ParseToken(refreshToken);
        if (refreshTokenClaims is null)
        {
            return BadRequest("Invalid refresh token was provided.");
        }

        var refreshTokenId = Guid.Parse(refreshTokenClaims["jti"]);
        var refreshTokenEntity = await _context.RefreshTokens
            .SingleOrDefaultAsync(rt => rt.Id == refreshTokenId);
        if (refreshTokenEntity is null)
        {
            return Conflict("Provided refresh token has already been used.");
        }
        
        _context.RefreshTokens.Remove(refreshTokenEntity);
        await _context.SaveChangesAsync();

        var userId = Guid.Parse(refreshTokenClaims["sub"]);
        var refreshTokenLifetime = int.Parse(_configuration["JwtAuth:RefreshTokenLifetime"]);
        List<Role> userRoles = refreshTokenClaims["role"]
            .Select(role => Enum.Parse<Role>(role.ToString()))
            .ToList();

        var newRefreshTokenEntity = new RefreshTokenEntity
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            ExpirationTime = DateTime.UtcNow.AddDays(refreshTokenLifetime)
        };
        _context.RefreshTokens.Add(newRefreshTokenEntity);
        await _context.SaveChangesAsync();

        var tokenPair = _jwtTokenHelper.IssuerTokenPair(userId, newRefreshTokenEntity.Id,userRoles);
        var tokenPairDto = tokenPair.Map();
        return Ok(tokenPairDto);
    }
}