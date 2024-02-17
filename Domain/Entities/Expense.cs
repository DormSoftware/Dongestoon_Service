using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.UserEntity;
using Domain.Enums;

namespace Domain.Entities;

public class Expense
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { set; get; }

    public User ExpenUser { get; set; }

    // INFO ABOUT EXPENSE
    public DateTime Date { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }

    public Group Group { get; set; }
}