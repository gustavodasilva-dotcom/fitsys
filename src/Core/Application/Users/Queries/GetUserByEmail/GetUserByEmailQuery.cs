﻿using Domain.Entities;
using MediatR;

namespace Application.Users.Queries.GetUserByEmail
{
    public sealed record GetUserByEmailQuery(string Email) : IRequest<User>;
}
