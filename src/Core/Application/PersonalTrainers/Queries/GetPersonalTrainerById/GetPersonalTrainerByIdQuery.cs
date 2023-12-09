using Domain.Entities;
using MediatR;

namespace Application.PersonalTrainers.Queries.GetPersonalTrainerById;

public sealed record GetPersonalTrainerByIdQuery(Guid UID) : IRequest<PersonalTrainer>;