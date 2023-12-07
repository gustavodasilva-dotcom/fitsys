﻿using Domain.Entities;
using MongoDB.Driver;

namespace Domain.Abstractions
{
    public interface IMongoContext
    {
        IMongoCollection<Client> Clients { get; }
    }
}
