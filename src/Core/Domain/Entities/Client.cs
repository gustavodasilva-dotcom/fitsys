using Domain.Primitives;
using MongoDB.Bson;

namespace Domain.Entities
{
    public sealed class Client : Entity
    {
        public Client(ObjectId id, decimal weight, decimal height, DateTime birthday)
            : base(id)
        {
            Weight = weight;
            Height = height;
            Birthday = birthday;
        }

        private Client()
        { 
        }

        public decimal Weight { get; set; }

        public decimal Height { get; set; }

        public DateTime Birthday { get; set; }
    }
}
