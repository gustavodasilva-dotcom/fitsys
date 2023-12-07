using Application.Exceptions.Base;

namespace Application.Exceptions
{
    public sealed class ClientNotFoundException(string reference)
        : BaseException($"Client not found with the reference {reference}")
    {
    }
}
