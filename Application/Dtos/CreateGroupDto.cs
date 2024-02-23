namespace Application.Dtos;

public struct CreateGroupDto
{
    public required string Name { get; init; }
    public required List<string> Users { get; init; }
}