using Domain.Enums;

namespace Application.Dtos;

public struct UserDto
{
    public required string UserName { get; set; }
    public required string LastName { get; set; }
    public required string Name { get; set; }
    public required Rank Rank { get; set; }
}