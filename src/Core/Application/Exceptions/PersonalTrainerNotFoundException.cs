using Application.Exceptions.Base;

namespace Application.Exceptions
{
    public sealed class PersonalTrainerNotFoundException(string reference)
        : BaseException($"Personal trainer not found with the reference {reference}")
    {
    }
}
