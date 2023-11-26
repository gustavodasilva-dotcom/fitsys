using Application.Users.Commands.CreateUser;
using Application.Users.Queries.GetUserByEmail;
using Domain.Abstractions;
using Infrastructure;
using Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped(typeof(IMongoContext), typeof(MongoContext));
builder.Services.AddScoped(typeof(IUserRepository), typeof(UserRepository));

builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblies(
    typeof(CreateUserCommand).Assembly,
    typeof(GetUserByEmailQuery).Assembly
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
