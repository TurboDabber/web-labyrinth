using Microsoft.EntityFrameworkCore;
using LabyrinthApi.Domain.Entities;
using LabyrinthApi.Domain.Enums;
using LabyrinthApi.Infrastructure.Data;
using LabyrinthApi.Infrastructure.Repositories;


namespace LabyrinthApi.Tests.Infrastructure.Repositories;

public class DbMazeRepositoryTests
{
    private readonly MazeDbContext _context;
    private readonly DbMazeRepository _repository;
    private readonly Maze _maze;

    public DbMazeRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<MazeDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new MazeDbContext(options);
        _repository = new DbMazeRepository(_context);

        _maze = new Maze
        {
            Width = 10,
            Height = 10,
            MazeDataJson = "[[0, 1], [1, 0]]",
            AlgorithmType = MazeAlgorithmType.RecursiveBacktracking
        };

        _context.Mazes.Add(_maze);
        _context.SaveChanges();
    }

    [Fact]
    public async Task AddAsync_Should_Add_Maze_To_Database()
    {
        var maze = new Maze
        {
            Width = 10,
            Height = 10,
            MazeDataJson = "[[0, 1], [1, 0]]",
            AlgorithmType = MazeAlgorithmType.RecursiveBacktracking
        };

        await _repository.AddAsync(maze);

        var savedMaze = await _context.Mazes.FindAsync(maze.Id);
        Assert.NotNull(savedMaze);
        Assert.True(savedMaze.Id > 0);
        Assert.Equal(maze.Width, savedMaze.Width);
        Assert.Equal(maze.Height, savedMaze.Height);
        Assert.Equal(maze.MazeDataJson, savedMaze.MazeDataJson);
        Assert.Equal(maze.AlgorithmType, savedMaze.AlgorithmType);
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_Maze()
    {
        var maze = new Maze
        {
            Width = 10,
            Height = 10,
            MazeDataJson = "[[0, 1], [1, 0]]",
            AlgorithmType = MazeAlgorithmType.RecursiveBacktracking
        };

        _context.Mazes.Add(maze);
        await _context.SaveChangesAsync();

        var result = await _repository.GetByIdAsync(maze.Id);

        Assert.NotNull(result);
        Assert.Equal(maze.Id, result.Id);
        Assert.Equal(maze.Width, result.Width);
        Assert.Equal(maze.Height, result.Height);
        Assert.Equal(maze.MazeDataJson, result.MazeDataJson);
        Assert.Equal(maze.AlgorithmType, result.AlgorithmType);
    }
}