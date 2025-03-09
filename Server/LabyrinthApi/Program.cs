using Microsoft.EntityFrameworkCore;
using LabyrinthApi.Infrastructure.Data;
using LabyrinthApi.Domain.Interfaces;
using LabyrinthApi.Application.Services;
using LabyrinthApi.Infrastructure.Repositories;
using LabyrinthApi.Application.Queries;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MazeDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("mainDb")));

builder.Services.AddScoped<IMazeRepository, DbMazeRepository>();
builder.Services.AddScoped<IMazeGenerator, RecursiveBacktrackingMazeGenerator>();
builder.Services.AddScoped<IPathFinder, DijkstraPathFinder>();
builder.Services.AddScoped<IMazeService, MazeService>();

builder.Services.AddTransient<GetMazeByIdQuery>();
builder.Services.AddTransient<GenerateMazeQuery>();
builder.Services.AddTransient<GetPathQuery>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

var app = builder.Build();

app.MapControllers();

app.Run();