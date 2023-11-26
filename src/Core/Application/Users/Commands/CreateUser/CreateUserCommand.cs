using MediatR;
using MongoDB.Bson;

namespace Application.Users.Commands.CreateUser
{
    public sealed record CreateUserCommand(string Name, string Email, string Password) : IRequest<ObjectId>;
}
