using Application.Accounts.Commands.ExecuteLogin;
using Application.Clients.Commands.CreateClient;
using Application.Clients.Commands.UpdateClient;
using Application.Clients.Queries.GetAllClients;
using Application.Clients.Queries.GetClientById;
using Application.Constants.Queries.GetConstantByEnum;
using Application.Exercises.Commands.CreateExercise;
using Application.Exercises.Commands.UpdateExercise;
using Application.Exercises.Queries.GetAllExercises;
using Application.Exercises.Queries.GetExerciseById;
using Application.Files.Commands.UploadFile;
using Application.Employees.Commands.CreateEmployee;
using Application.Employees.Commands.UpdateEmployee;
using Application.Employees.Queries.GetAllEmployees;
using Application.Employees.Queries.GetEmployeeById;
using Domain.Abstractions;
using Infrastructure;
using Infrastructure.Repositories;
using Infrastructure.Seeders;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.SlidingExpiration = true;
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/Forbidden/";
    });

builder.Services.AddSingleton(typeof(IMongoContext), typeof(MongoContext));
builder.Services.AddSingleton(typeof(IConstantsRepository), typeof(ConstantsRepository));
builder.Services.AddSingleton(typeof(IClientsRepository), typeof(ClientsRepository));
builder.Services.AddSingleton(typeof(IEmployeesRepository), typeof(EmployeesRepository));
builder.Services.AddSingleton(typeof(IExercisesRepository), typeof(ExercisesRepository));

builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblies(
    typeof(ExecuteLoginCommand).Assembly,
    typeof(GetAllClientsQuery).Assembly,
    typeof(CreateClientCommand).Assembly,
    typeof(GetClientByIdQuery).Assembly,
    typeof(UploadFileCommand).Assembly,
    typeof(UpdateClientCommand).Assembly,
    typeof(GetAllEmployeesQuery).Assembly,
    typeof(GetEmployeeByIdQuery).Assembly,
    typeof(CreateEmployeeCommand).Assembly,
    typeof(UpdateEmployeeCommand).Assembly,
    typeof(GetConstantByEnumQuery).Assembly,
    typeof(GetAllExercisesQuery).Assembly,
    typeof(GetExerciseById).Assembly,
    typeof(CreateExerciseCommand).Assembly,
    typeof(UpdateExerciseCommand).Assembly
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

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
