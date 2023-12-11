using Domain.Entities;
using MediatR;

namespace Application.Exercises.Queries.GetAllExercises;

public sealed record GetAllExercisesQuery() : IRequest<List<Exercise>>;