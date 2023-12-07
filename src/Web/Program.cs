using Application.Clients.Commands.CreateClient;
using Application.Clients.Commands.UpdateClient;
using Application.Clients.Queries.GetAllClients;
using Application.Clients.Queries.GetClientById;
using Application.Files.Commands.UploadFile;
using Domain.Abstractions;
using Infrastructure;
using Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton(typeof(IMongoContext), typeof(MongoContext));
builder.Services.AddSingleton(typeof(IClientsRepository), typeof(ClientsRepository));

builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblies(
    typeof(GetAllClientsQuery).Assembly,
    typeof(CreateClientCommand).Assembly,
    typeof(GetClientByIdQuery).Assembly,
    typeof(UploadFileCommand).Assembly,
    typeof(UpdateClientCommand).Assembly
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
