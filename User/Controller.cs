using System.IdentityModel.Tokens.Jwt;
using System.Net.Mime;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Orders.Helpers;

namespace Orders.User;

[ApiController]
[Route("/users")]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
public class UserController : ControllerBase
{
    private readonly DatabaseContext _context;
    private readonly IConfiguration _configuration;
    private readonly JwtTokenHelper _jwtTokenHelper;

    public UserController(DatabaseContext context, IConfiguration configuration, JwtTokenHelper jwtTokenHelper)
    {
        _context = context;
        _configuration = configuration;
        _jwtTokenHelper = jwtTokenHelper;
    }

    [HttpGet("who-am-i")]
    [Authorize]
    public async Task<IActionResult> GetCurrentUserInfo()
    {
        var subClaim = User.Claims.Single(claim => claim.Type == "sub");
        var currentUserId = Guid.Parse(subClaim.Value);

        var currentUser = await _context.Users.SingleAsync(user => user.Id == currentUserId);

        var readDto = new ReadUserDto
        {
            Id = currentUser.Id,
            DisplayName = currentUser.DisplayName,
            Username = currentUser.Username
        };

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

    [HttpPost("authenticate")]
    public async Task<IActionResult> AuthenticateUser([FromBody] AuthenticateUserDto authUserDto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == authUserDto.Username);
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
        await _context.SaveChangesAsync();

        var tokenPair = _jwtTokenHelper.IssuerTokenPair(user.Id, refreshTokenEntity.Id);
        var tokenPairDto = new TokenPairDto
        {
            AccessToken = tokenPair.AccessToken,
            RefreshToken = tokenPair.RefreshToken
        };
        return Ok(tokenPairDto);
    }

    [HttpPost("refresh")]
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
        var refreshTokenLifetime = int.Parse(_configuration["JwtAuth:RefreshTokenLifetim"]);
        var newRefreshTokenEntity = new RefreshTokenEntity
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            ExpirationTime = DateTime.UtcNow.AddDays(refreshTokenLifetime)
        };
        _context.RefreshTokens.Add(newRefreshTokenEntity);
        await _context.SaveChangesAsync();

        var tokenPair = _jwtTokenHelper.IssuerTokenPair(userId, newRefreshTokenEntity.Id);
        var tokenPairDto = new TokenPairDto
        {
            AccessToken = tokenPair.AccessToken,
            RefreshToken = tokenPair.RefreshToken
        };
        return Ok(tokenPairDto);
    }
}