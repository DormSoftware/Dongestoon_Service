using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.UserEntity;

namespace Domain.Entities;

public class Expense
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { set; get; }

    public required Guid UserId { get; set; }
    public User? User { get; set; }

    // INFO ABOUT EXPENSE
    public required DateTime Date { get; set; } = DateTime.Now;
    public required string Title { get; set; }
    public string? Description { get; set; }
    public required decimal Amount { get; set; }

    public required Guid GroupId { get; set; }
    public Group? Group { get; set; }
}