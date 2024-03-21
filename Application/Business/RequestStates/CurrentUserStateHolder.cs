using Application.Abstractions;
using Domain.Entities.UserEntity;
using Domain.Exceptions;

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