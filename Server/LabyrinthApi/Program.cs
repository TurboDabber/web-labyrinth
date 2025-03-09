using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using LabyrinthApi.Infrastructure.Data;
using LabyrinthApi.Domain.Interfaces;
using LabyrinthApi.Application.Services;
using LabyrinthApi.Infrastructure.Repositories;
using LabyrinthApi.Application.Queries.GenerateMazeQuery;
using LabyrinthApi.Application.Queries.GetMazeByIdQuery;
using LabyrinthApi.Application.Queries.GetPathQuery;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MazeDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("mainDb")));

builder.Services.AddScoped<IMazeRepository, DbMazeRepository>();
builder.Services.AddScoped<IMazeGenerator, RecursiveBacktrackingMazeGenerator>();
builder.Services.AddScoped<IPathFinder, DijkstraPathFinder>();
builder.Services.AddScoped<IMazeService, MazeService>();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Labyrinth API", Version = "v1" });
});

builder.Services.AddTransient<GetMazeByIdQuery>();
builder.Services.AddTransient<GenerateMazeQuery>();
builder.Services.AddTransient<GetPathQuery>();


builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Labyrinth API v1"));
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapControllers();

app.Run();