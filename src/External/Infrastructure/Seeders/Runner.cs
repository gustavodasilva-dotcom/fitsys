using Domain.Enums;
using Humanizer;
using MongoDB.Driver;

namespace Infrastructure.Seeders;

public static class Runner
{
    public static async Task ExecuteAsync(string connectionString, string database)
    {
        MongoClient client = new(connectionString);
        IMongoDatabase db = client.GetDatabase(database);
        
        //await ShiftSeeder(db);
    }

    // private static async Task ShiftSeeder(IMongoDatabase database)
    // {
    //     try
    //     {
    //         var collection = database.GetCollection<ShiftsEnum>("shifts");

    //         foreach (string value in Enum.GetValues(typeof(ShiftsEnum)))
    //         {
    //             await collection.FindAsync(p => p.)
    //         }
    //     }
    //     catch (Exception e)
    //     {

    //     }
    // }
}