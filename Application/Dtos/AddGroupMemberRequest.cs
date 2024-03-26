namespace Application.Dtos;

public class AddGroupMemberRequest
{
    public Guid GroupId { get; set; }
    public Guid UserId { get; set; }
}