using Domain.Entities;
using MediatR;

namespace Application.Exercises.Queries.GetExerciseById;

public sealed record GetExerciseById(Guid UID) : IRequest<Exercise>;