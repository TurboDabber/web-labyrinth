using LabyrinthApi.Application.Services;
using LabyrinthApi.Domain.Entities;
using LabyrinthApi.Domain.Enums;
using LabyrinthApi.Domain.Interfaces;
using Moq;

namespace LabyrinthApi.Tests.Application.Services;

public class MazeServiceTests
{
    private readonly Mock<IMazeRepository> _mockMazeRepository;
    private readonly Mock<IMazeGenerator> _mockMazeGenerator;
    private readonly Mock<IPathFinder> _mockPathFinder;
    private readonly MazeService _mazeService;

    public MazeServiceTests()
    {
        _mockMazeRepository = new Mock<IMazeRepository>();
        _mockMazeGenerator = new Mock<IMazeGenerator>();
        _mockPathFinder = new Mock<IPathFinder>();
        _mazeService = new MazeService(_mockMazeRepository.Object, _mockMazeGenerator.Object, _mockPathFinder.Object);
    }

    [Fact]
    public async Task GenerateMazeAsync_Should_Return_MazeId()
    {
        int width = 10;
        int height = 10;
        var algorithmType = MazeAlgorithmType.RecursiveBacktracking;
        var mazeData = new int[height, width];
        var maze = new Maze
        {
            Id = 1,
            Width = width,
            Height = height,
            MazeDataJson = "[[0, 1], [1, 0]]",
            AlgorithmType = algorithmType
        };

        _mockMazeGenerator.Setup(g => g.GenerateMaze(width, height)).Returns(mazeData);
        _mockMazeRepository.Setup(r => r.AddAsync(It.IsAny<Maze>())).ReturnsAsync(maze.Id);

        var result = await _mazeService.GenerateMazeAsync(width, height, algorithmType);

        Assert.Equal(maze.Id, result);
        _mockMazeGenerator.Verify(g => g.GenerateMaze(width, height), Times.Once);
        _mockMazeRepository.Verify(r => r.AddAsync(It.IsAny<Maze>()), Times.Once);
    }

    [Fact]
    public async Task SolveMazeAsync_Should_Return_Path()
    {
        int mazeId = 1;
        var start = (0, 0);
        var end = (1, 1);
        var maze = new Maze
        {
            Id = mazeId,
            Width = 10,
            Height = 10,
            MazeDataJson = "[[0, 1], [1, 0]]",
            AlgorithmType = MazeAlgorithmType.RecursiveBacktracking
        };
        var path = new List<(int, int)> { start, end };

        _mockMazeRepository.Setup(r => r.GetByIdAsync(mazeId)).ReturnsAsync(maze);
        _mockPathFinder.Setup(p => p.FindPath(It.IsAny<int[,]>(), start, end)).Returns(path);


        var result = await _mazeService.SolveMazeAsync(mazeId, start, end);

        Assert.Equal(path, result);
        _mockMazeRepository.Verify(r => r.GetByIdAsync(mazeId), Times.Once);
        _mockPathFinder.Verify(p => p.FindPath(It.IsAny<int[,]>(), start, end), Times.Once);
    }
}