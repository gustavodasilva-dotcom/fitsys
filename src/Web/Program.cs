using Application.Clients.Commands.CreateClient;
using Application.Clients.Commands.UpdateClient;
using Application.Clients.Queries.GetAllClients;
using Application.Clients.Queries.GetClientById;
using Application.Constants.Queries.GetConstantByEnum;
using Application.Files.Commands.UploadFile;
using Application.PersonalTrainers.Commands.CreatePersonalTrainer;
using Application.PersonalTrainers.Commands.UpdatePersonalTrainer;
using Application.PersonalTrainers.Queries.GetAllPersonalTrainers;
using Application.PersonalTrainers.Queries.GetPersonalTrainerById;
using Domain.Abstractions;
using Infrastructure;
using Infrastructure.Repositories;
using Infrastructure.Seeders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton(typeof(IMongoContext), typeof(MongoContext));
builder.Services.AddSingleton(typeof(IConstantsRepository), typeof(ConstantsRepository));
builder.Services.AddSingleton(typeof(IClientsRepository), typeof(ClientsRepository));
builder.Services.AddSingleton(typeof(IPersonalTrainersRepository), typeof(PersonalTrainersRepository));

builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblies(
    typeof(GetAllClientsQuery).Assembly,
    typeof(CreateClientCommand).Assembly,
    typeof(GetClientByIdQuery).Assembly,
    typeof(UploadFileCommand).Assembly,
    typeof(UpdateClientCommand).Assembly,
    typeof(GetAllPersonalTrainersQuery).Assembly,
    typeof(GetPersonalTrainerByIdQuery).Assembly,
    typeof(CreatePersonalTrainerCommand).Assembly,
    typeof(UpdatePersonalTrainerCommand).Assembly,
    typeof(GetConstantByEnumQuery).Assembly
    ));

var connectionString = builder.Configuration["MongoDB:ConnectionString"];
var database = builder.Configuration["MongoDB:Database"];
Runner.ExecuteAsync(connectionString!, database!).Wait();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
