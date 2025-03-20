using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using LabyrinthApi.Infrastructure.Data;
using LabyrinthApi.Domain.Interfaces;
using LabyrinthApi.Application.Services;
using LabyrinthApi.Infrastructure.Repositories;
using LabyrinthApi.Application.Queries.GenerateMazeQuery;
using LabyrinthApi.Application.Queries.GetMazeByIdQuery;
using LabyrinthApi.Application.Queries.GetPathQuery;

const string AllowedOrigin = "_angularOrigin";
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
    c.SchemaFilter<FixMissingSwaggerProperties>();
});

builder.Services.AddTransient<GetMazeByIdQuery>();
builder.Services.AddTransient<GenerateMazeQuery>();
builder.Services.AddTransient<GetPathQuery>();


builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowedOrigin,
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseCors(AllowedOrigin);
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Labyrinth API v1"));
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();