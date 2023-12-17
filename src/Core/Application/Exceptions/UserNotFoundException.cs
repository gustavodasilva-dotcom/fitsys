using Application.Exceptions.Base;

namespace Application.Exceptions
{
    public sealed class UserNotFoundException(string email)
        : BaseException($"User not found with the email {email}. Please, check your credentials.")
    {
    }
}
