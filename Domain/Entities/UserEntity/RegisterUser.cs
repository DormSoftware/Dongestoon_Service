using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.UserEntity;

public class RegisterUser
{
    public string Username { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    [EmailAddress] public string Email { get; set; }
    public string Password { get; set; } = "";
}