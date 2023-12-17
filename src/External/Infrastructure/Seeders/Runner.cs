using Domain.Entities;
using Domain.Entities.Partials;
using Domain.Enums;
using Humanizer;
using MongoDB.Bson;
using MongoDB.Driver;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Infrastructure.Seeders;

public static class Runner
{
    public static async Task ExecuteAsync(string connectionString, string database)
    {
        MongoClient client = new(connectionString);
        IMongoDatabase db = client.GetDatabase(database);

        await SeedConstants(db);
        await SeedUserAdmin(db);
    }

    private static async Task SeedConstants(IMongoDatabase database)
    {
        try
        {
            var collection = database.GetCollection<Constant>(nameof(Constant).Pluralize());

            Constant constant = collection.FindSync(c => c.key == (int)ConstantsEnum.Roles).FirstOrDefault();
            if (constant == null)
            {
                constant = new(
                    id: ObjectId.GenerateNewId(),
                    uid: Guid.NewGuid(),
                    key: (int)ConstantsEnum.Roles,
                    name: Enum.GetName(ConstantsEnum.Roles)!
                );
                await collection.InsertOneAsync(constant);
            }

            Type roles = typeof(RolesEnum);
            foreach (var role in roles.GetEnumValues())
            {
                MemberInfo info = roles.GetMember(role.ToString()!).First();
                var name = info?.GetCustomAttribute<DisplayAttribute>()?.Name ?? info!.Name;

                ConstantValue? value = constant.values.FirstOrDefault(c => c.value == (int)role);
                if (value == null)
                {
                    value = new ConstantValue(
                        id: ObjectId.GenerateNewId(),
                        uid: Guid.NewGuid(),
                        value: (int)role,
                        description: name
                    );
                    constant.values.Add(value);
                }
            }
            collection.ReplaceOne(c => c.id.Equals(constant.id), constant);

            constant = collection.FindSync(c => c.key == (int)ConstantsEnum.Shifts).FirstOrDefault();
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

            Type shifts = typeof(ShiftsEnum);
            foreach (var shift in shifts.GetEnumValues())
            {
                MemberInfo info = shifts.GetMember(shift.ToString()!).First();
                var name = info?.GetCustomAttribute<DisplayAttribute>()?.Name ?? info!.Name;

                ConstantValue? value = constant.values.FirstOrDefault(c => c.value == (int)shift);
                if (value == null)
                {
                    value = new ConstantValue(
                        id: ObjectId.GenerateNewId(),
                        uid: Guid.NewGuid(),
                        value: (int)shift,
                        description: name
                    );
                    constant.values.Add(value);
                }
            }
            collection.ReplaceOne(c => c.id.Equals(constant.id), constant);

            constant = collection.FindSync(c => c.key == (int)ConstantsEnum.MuscleGroups).FirstOrDefault();
            if (constant == null)
            {
                constant = new(
                    id: ObjectId.GenerateNewId(),
                    uid: Guid.NewGuid(),
                    key: (int)ConstantsEnum.MuscleGroups,
                    name: Enum.GetName(ConstantsEnum.MuscleGroups)!
                );
                await collection.InsertOneAsync(constant);
            }

            Type muscles = typeof(MuscleGroupsEnum);
            foreach (var muscle in muscles.GetEnumValues())
            {
                MemberInfo info = muscles.GetMember(muscle.ToString()!).First();
                var name = info?.GetCustomAttribute<DisplayAttribute>()?.Name ?? info!.Name;

                ConstantValue? value = constant.values.FirstOrDefault(c => c.value == (int)muscle);
                if (value == null)
                {
                    value = new ConstantValue(
                        id: ObjectId.GenerateNewId(),
                        uid: Guid.NewGuid(),
                        value: (int)muscle,
                        description: name
                    );
                    constant.values.Add(value);
                }
            }
            collection.ReplaceOne(c => c.id.Equals(constant.id), constant);
        }
        catch (Exception)
        {
        }
    }

    private static async Task SeedUserAdmin(IMongoDatabase database)
    {
        try
        {
            const string email = "admin@fitsys.com";
            const string profile = "img/logo-transparent-large.png";

            DateTime now = DateTime.Now;
            string password = BCrypt.Net.BCrypt.HashPassword($"Admin{now.Year}@");

            var employeeCollection = database.GetCollection<Employee>(nameof(Employee).Pluralize());

            Employee admin = employeeCollection.FindSync(e => e.user != null && e.user.email.Equals(email)).FirstOrDefault();
            if (admin == null)
            {
                var constantCollection = database.GetCollection<Constant>(nameof(Constant).Pluralize());
                var shifts = constantCollection.FindSync(e => e.key == (int)ConstantsEnum.Shifts).FirstOrDefault();
                var roles = constantCollection.FindSync(e => e.key == (int)ConstantsEnum.Roles).FirstOrDefault();

                admin = new(
                    id: ObjectId.GenerateNewId(),
                    uid: Guid.NewGuid(),
                    shifts: shifts.values,
                    person: new Person(
                        name: "FitSys Admin",
                        birthday: now,
                        profile: profile),
                    user: new User(
                        email: email,
                        password: password,
                        role: roles.values.FirstOrDefault(r => r.value == (int)RolesEnum.Admin))
                );
                await employeeCollection.InsertOneAsync(admin);
            }
            else
            {
                admin.user.SetEmail(email);
                admin.user.SetPassword(password);
                employeeCollection.ReplaceOne(c => c.id.Equals(admin.id), admin);
            }
        }
        catch (Exception)
        {
        }
    }
}