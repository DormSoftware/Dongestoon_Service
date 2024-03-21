using Domain.Entities.UserEntity;

namespace Application.Abstractions;

public interface ICurrentUserStateHolder
{
    User GetCurrentUser();
    void SetUser(User user);
}