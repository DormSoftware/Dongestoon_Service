namespace Application.Dtos;

public struct GroupsDto
{
    public string Name { get; set; }
    public Guid OwnerId { get; set; }
    public List<Guid> Users { get; set; }
}