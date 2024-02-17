using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.UserEntity;
using Domain.Enums;

namespace Domain.Entities;

public class Group
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { set; get; }

    public string Name { get; set; }
    public List<User> Users { get; set; } = [];
    public Guid? ProfilePic { get; set; }
    public GroupRank Rank { get; set; }
    public decimal TotalCost { get; set; }

    public ICollection<Expense> Expenses { get; set; }
}