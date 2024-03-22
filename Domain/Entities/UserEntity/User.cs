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

    public decimal MoneySpent { get; set; } = 0;
    public decimal MoneyReceived { get; set; } = 0;

    public Rank Rank { get; set; } = Rank.STARTER;
    public bool IsActive { get; set; } = false;

    public List<Group> Groups { get; set; } = [];

    public User(string username, string name, string lastName, string email, string password)
    {
        Username = username;
        Name = name;
        LastName = lastName;
        Email = email;
        Password = password;
    }

    public User()
    {
    }
}