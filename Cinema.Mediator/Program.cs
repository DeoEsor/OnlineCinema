using Cinema.Mediator.Services;
using Cinema.Users.Repository.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<UsersRepository>();
builder.Services.AddGrpc();
var app = builder.Build();
app.Urls.Add("https://localhost:7280");
// Configure the HTTP request pipeline.
app.MapGrpcService<UsersService>();
app.Run();