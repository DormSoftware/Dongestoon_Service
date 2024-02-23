using System.Runtime.Serialization;

namespace Domain.Exceptions;

[Serializable]
public class NoUserFoundWithGivenIdException : Exception
{
    public NoUserFoundWithGivenIdException(Guid userId) : base($"no user FoundWith Given id {userId}")
    {
    }

    public NoUserFoundWithGivenIdException()
    {
    }

    protected NoUserFoundWithGivenIdException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public NoUserFoundWithGivenIdException(string? message) : base(message)
    {
    }

    public NoUserFoundWithGivenIdException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}