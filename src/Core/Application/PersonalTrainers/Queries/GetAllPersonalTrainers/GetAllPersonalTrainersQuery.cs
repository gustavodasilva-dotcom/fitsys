using Domain.Entities;
using MediatR;

namespace Application.PersonalTrainers.Queries.GetAllPersonalTrainers;

public sealed record GetAllPersonalTrainersQuery(int Skip = 0, int Limit = 50) : IRequest<List<PersonalTrainer>>;