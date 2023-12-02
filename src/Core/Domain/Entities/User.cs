﻿using Domain.Primitives;
using MongoDB.Bson;

namespace Domain.Entities
{
    public sealed class User : Entity
    {
        public User(ObjectId id, string name, string email, string password)
            : base(id)
        {
            Name = name.Trim();
            Email = email.Trim();
            Password = password.Trim();
        }

        private User()
        {
        }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public string Password { get; private set; }
    }
}