﻿using Domain.Models;
using MediatR;
using MongoDB.Bson;

namespace Application.Clients.Commands.CreateClient;

public sealed record CreateClientCommand(
    string Name,
    string Email,
    string Password,
    decimal Weight,
    decimal Height,
    DateTime Birthday,
    string? Profile,
    List<WorkoutInputModel> Workouts) : IRequest<ObjectId>;
