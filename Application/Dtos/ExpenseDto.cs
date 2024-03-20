namespace Application.Dtos;

public struct ExpenseDto
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public UserDto User { get; set; }
    public GroupsDto Group { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public required decimal Amount { get; set; }
}