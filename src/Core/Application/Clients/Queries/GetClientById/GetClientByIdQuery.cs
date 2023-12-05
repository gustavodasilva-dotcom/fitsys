using Domain.Entities;
using MediatR;
using MongoDB.Bson;

namespace Application.Clients.Queries.GetClientById
{
    public sealed record GetClientByIdQuery(ObjectId Id) : IRequest<Person>;
}
