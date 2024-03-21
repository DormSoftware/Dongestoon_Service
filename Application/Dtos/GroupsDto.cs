using Domain.Enums;

namespace Application.Dtos;

public struct GroupsDto
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required Guid OwnerId { get; set; }
    public required List<Guid> Users { get; set; }
    public required GroupRank Rank { get; set; }
    public required decimal TotalCost { get; set; }
    public Guid? ProfilePic { get; set; }
}