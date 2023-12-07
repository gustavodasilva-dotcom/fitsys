using Application.Exceptions.Base;

namespace Application.Exceptions
{
    public sealed class ClientConflictException(string value)
        : BaseException($"There's already a client registered with the property {value}")
    {
    }
}
