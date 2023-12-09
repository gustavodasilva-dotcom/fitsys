using Application.Exceptions.Base;

namespace Application.Exceptions
{
    public sealed class PersonalTrainerConflictException(string value)
        : BaseException($"There's already a personal trainer registered with the property {value}")
    {
    }
}
