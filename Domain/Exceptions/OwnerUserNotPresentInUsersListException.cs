using System.Runtime.Serialization;

namespace Domain.Exceptions;

[Serializable]
public class OwnerUserNotPresentInUsersListException : Exception
{
    public OwnerUserNotPresentInUsersListException(Guid ownerId) : base(
        $"Owner user must be in group users list {ownerId}")
    {
    }

    public OwnerUserNotPresentInUsersListException()
    {
    }

    protected OwnerUserNotPresentInUsersListException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public OwnerUserNotPresentInUsersListException(string? message) : base(message)
    {
    }

    public OwnerUserNotPresentInUsersListException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}