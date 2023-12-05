using Application.Exceptions.Base;

namespace Application.Exceptions
{
    public sealed class ClientNotFoundException(string name)
        : BaseException($"Client {name} not found")
    {
    }
}
