using Application.Exceptions.Base;

namespace Application.Exceptions
{
    public sealed class UserNotFoundException(string name)
        : BaseException($"User {name} not found")
    {
    }
}
