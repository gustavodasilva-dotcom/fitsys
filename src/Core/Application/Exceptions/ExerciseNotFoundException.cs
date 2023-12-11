using Application.Exceptions.Base;

namespace Application.Exceptions;

public sealed class ExerciseNotFoundException(string reference)
    : BaseException($"Exercise not found with the reference {reference}")
{
}