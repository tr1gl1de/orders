using Orders.Helpers;

namespace Orders.User;

public static class Extensions
{
    public static UserEntity Map(this RegisterUserDto registerUserDto)
        => new UserEntity
        {
            Id = Guid.NewGuid(),
            DisplayName = registerUserDto.DisplayName,
            Username = registerUserDto.Username,
            Password = BCrypt.Net.BCrypt.HashPassword(registerUserDto.Password)
        };
    
    public static ReadUserDto Map(this UserEntity userEntity)
        => new ReadUserDto
        {
            Id = userEntity.Id,
            DisplayName = userEntity.DisplayName,
            Username = userEntity.Username
        };

    public static TokenPairDto Map(this TokenPair tokenPair)
        => new TokenPairDto
        {
            AccessToken = tokenPair.AccessToken,
            RefreshToken = tokenPair.RefreshToken
        };
}