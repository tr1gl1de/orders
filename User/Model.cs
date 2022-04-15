using System.ComponentModel.DataAnnotations.Schema;

namespace Orders.User;

[Table("users")]
public class UserEntity
{
    [Column("id")]
    public Guid id { get; set; }
    
    [Column("display_name")]
    public string DisplayName { get; set; }
    
    [Column("username")]
    public string Username { get; set; }
    
    [Column("password")]
    public string Password { get; set; }
}