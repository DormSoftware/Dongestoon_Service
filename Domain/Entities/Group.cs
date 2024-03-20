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
    public Guid OwnerId { get; set; }
    public List<User> Users { get; set; } = new();
    public Guid? ProfilePic { get; set; }
    public GroupRank Rank { get; set; } = GroupRank.STARTER;
    public decimal TotalCost { get; set; } = 0;
    public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
}