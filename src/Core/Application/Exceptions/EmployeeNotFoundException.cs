using Application.Exceptions.Base;

namespace Application.Exceptions
{
    public sealed class EmployeeNotFoundException(string reference)
        : BaseException($"Employee not found with the reference {reference}")
    {
    }
}
