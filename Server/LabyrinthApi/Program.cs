using Microsoft.EntityFrameworkCore;
using LabyrinthApi.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MazeDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("mainDb")));

var app = builder.Build();

app.MapControllers();

app.Run();