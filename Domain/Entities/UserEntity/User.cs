using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;

namespace Domain.Entities.UserEntity;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [EmailAddress] public string Email { get; set; }

    public string Username { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Token { get; set; } = "";
    public string Password { get; set; } = "";

    public decimal MoneySpent { get; set; }
    public decimal MoneyReceived { get; set; }

    public Rank Rank { get; set; }
    public bool IsActive { get; set; } = false;

    public User(string username, string name, string lastName, string email, string password)
    {
        Username = username;
        Name = name;
        LastName = lastName;
        Email = email;
        Password = password;
    }
}