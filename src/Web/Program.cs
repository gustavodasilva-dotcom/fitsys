using Application.Clients.Commands.CreateClient;
using Application.Clients.Queries.GetAllClients;
using Application.Clients.Queries.GetClientById;
using Application.Files.Commands.UploadFile;
using Application.Users.Commands.CreateUser;
using Application.Users.Queries.GetUserByEmail;
using Domain.Abstractions;
using Infrastructure;
using Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton(typeof(IMongoContext), typeof(MongoContext));
builder.Services.AddSingleton(typeof(IPersonsRepository), typeof(PersonsRepository));

builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblies(
    typeof(CreateUserCommand).Assembly,
    typeof(GetUserByEmailQuery).Assembly,
    typeof(GetAllClientsQuery).Assembly,
    typeof(CreateClientCommand).Assembly,
    typeof(GetClientByIdQuery).Assembly,
    typeof(UploadFileCommand).Assembly
    ));

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
