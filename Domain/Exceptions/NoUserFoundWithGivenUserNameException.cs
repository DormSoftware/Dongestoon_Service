namespace Domain.Exceptions;

public class NoUserFoundWithGivenUserNameException : Exception
{
    public NoUserFoundWithGivenUserNameException(string userName) : base(
        $"There is no user with user name ({userName})")
    {
    }
}