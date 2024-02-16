using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;

namespace Domain.Entities;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public string Username { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    [EmailAddress] public string Email { get; set; }
    public Rank Rank { get; set; }
    public bool IsActive { get; set; } = false;
    public string Token { get; set; } = "";
    public string Password { get; set; } = "";

    public User(string username, string name, string lastName, string email, string password)
    {
        Username = username;
        Name = name;
        LastName = lastName;
        Email = email;
        Password = password;
    }
}