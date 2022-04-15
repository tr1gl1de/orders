using System.ComponentModel.DataAnnotations;

namespace Orders.User;

public class RegisterUserDto
{
    /// <summary>User display name</summary>
    /// <example>Jonh Doe</example>
    [Required]
    [MaxLength(50)]
    [RegularExpression(@"^[\w\s]*$")]
    public string DisplayName { get; set; }
    
    /// <summary>Username for authorization</summary>
    /// <example>jonh_doe333</example>
    [Required]
    [MaxLength(30)]
    [RegularExpression(@"^[\w\s\d]*$")]
    public string Username { get; set; }
    
    /// <summary>Password for authorization</summary>
    /// <example>pass333</example>
    [Required]
    [MinLength(6)]
    [MaxLength(20)]
    [RegularExpression(@"^[\w\s\d]*$")]
    public string Password { get; set; }
}

public class ReadUserDto
{
    /// <summary>User identifier</summary>
    /// <example>xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx</example>
    public Guid Id { get; set; }
    
    /// <summary>User display name</summary>
    /// <example>Jonh Doe</example>
    public string DisplayName { get; set; }
    
    /// <summary>Username for authorization</summary>
    /// <example>jonh_doe333</example>
    public string Username { get; set; }
}