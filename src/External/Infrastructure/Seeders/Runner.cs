using Domain.Entities;
using Domain.Enums;
using Humanizer;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Seeders;

public static class Runner
{
    public static async Task ExecuteAsync(string connectionString, string database)
    {
        MongoClient client = new(connectionString);
        IMongoDatabase db = client.GetDatabase(database);

        await ConstantSeeder(db);
    }

    private static async Task ConstantSeeder(IMongoDatabase database)
    {
        try
        {
            var collection = database.GetCollection<Constant>(nameof(Constant).Pluralize());

            Constant constant = collection.FindSync(c => c.key == (int)ConstantsEnum.Shifts).FirstOrDefault();
            if (constant == null)
            {
                constant = new(
                    id: ObjectId.GenerateNewId(),
                    uid: Guid.NewGuid(),
                    key: (int)ConstantsEnum.Shifts,
                    name: Enum.GetName(ConstantsEnum.Shifts)!
                );
                await collection.InsertOneAsync(constant);
            }

            string[] shifts = ["Morning", "Afternoon", "Night"];
            shifts.ToList().ForEach(shift =>
            {
                ConstantValue? value = constant.values.FirstOrDefault(c => c.value.Equals(shift));
                if (value == null)
                {
                    value = new ConstantValue(
                        id: ObjectId.GenerateNewId(),
                        uid: Guid.NewGuid(),
                        value: shift
                    );
                    constant.values.Add(value);
                }
            });
            collection.ReplaceOne(c => c.id.Equals(constant.id), constant);

            constant = collection.FindSync(c => c.key == (int)ConstantsEnum.Muscle).FirstOrDefault();
            if (constant == null)
            {
                constant = new(
                    id: ObjectId.GenerateNewId(),
                    uid: Guid.NewGuid(),
                    key: (int)ConstantsEnum.Muscle,
                    name: Enum.GetName(ConstantsEnum.Muscle)!
                );
                await collection.InsertOneAsync(constant);
            }

            string[] muscles = ["Calves", "Hamstrings", "Quadriceps", "Glutes", "Biceps", "Triceps", "Forearms", "Trapezius", "Latissimus Dorsi"];
            muscles.ToList().ForEach(muscle =>
            {
                ConstantValue? value = constant.values.FirstOrDefault(c => c.value.Equals(muscle));
                if (value == null)
                {
                    value = new ConstantValue(
                        id: ObjectId.GenerateNewId(),
                        uid: Guid.NewGuid(),
                        value: muscle
                    );
                    constant.values.Add(value);
                }
            });
            collection.ReplaceOne(c => c.id.Equals(constant.id), constant);
        }
        catch (Exception e)
        {

        }
    }
}