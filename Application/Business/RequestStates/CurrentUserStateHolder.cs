using Domain.Entities.UserEntity;

namespace Application.Business.RequestStates;

public class CurrentUserStateHolder : ICurrentUserStateHolder
{
    private User? _user;

    public User GetCurrentUser()
    {
        if (_user == null)
        {
            throw new NoLoggedInUserException();
        }

        return _user;
    }

    public void SetUser(User user)
    {
        _user = user;
    }
}

public class NoLoggedInUserException : Exception
{
}

public interface ICurrentUserStateHolder
{
    User GetCurrentUser();
    void SetUser(User user);
}