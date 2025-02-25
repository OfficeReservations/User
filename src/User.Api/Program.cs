using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using User;
using User.Api.Services;
using User.Application;
using User.Application.Command;
using User.Domain.Data;

const int GrpcPort = 28711;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UserDbContext>(
    options => options.UseNpgsql(
        builder.Configuration.GetConnectionString("UserServiceConnection")
    )
);

builder.Services.AddApplication();

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddScoped<UserService.UserServiceBase, UserGrpcService>();



builder.WebHost.UseKestrel(options =>
{
    options.AddServerHeader = false;
    options.ListenAnyIP(GrpcPort, opt => opt.Protocols = HttpProtocols.Http2);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGrpcService<UserGrpcService>();

app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");


app.Run();