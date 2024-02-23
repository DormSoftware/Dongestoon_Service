using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { set; get; }

    public decimal Price { get; set; }
    public string Name { get; set; }

    public Category? Category { get; set; }
    public Guid? CategoryId { get; set; }
}