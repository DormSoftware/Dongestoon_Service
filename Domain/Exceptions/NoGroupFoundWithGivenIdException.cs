using System.Runtime.Serialization;

namespace Domain.Exceptions;

[Serializable]
public class NoGroupFoundWithGivenIdException : Exception
{
    public NoGroupFoundWithGivenIdException()
    {
    }

    protected NoGroupFoundWithGivenIdException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public NoGroupFoundWithGivenIdException(string? message) : base(message)
    {
    }

    public NoGroupFoundWithGivenIdException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}