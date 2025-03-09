using LabyrinthApi.Domain.Entities;
using LabyrinthApi.Domain.Enums;
using LabyrinthApi.Domain.Interfaces;
using LabyrinthApi.Domain.Other;
using Newtonsoft.Json;

namespace LabyrinthApi.Application.Services;

public class MazeService : IMazeService
{
    private readonly IMazeRepository _mazeRepository;
    private readonly IMazeGenerator _mazeGenerator;
    private readonly IPathFinder _pathFinder;

    public MazeService(IMazeRepository mazeRepository, IMazeGenerator mazeGenerator, IPathFinder pathFinder)
    {
        _mazeRepository = mazeRepository;
        _mazeGenerator = mazeGenerator;
        _pathFinder = pathFinder;
    }

    public async Task<int> GenerateMazeAsync(int width, int height, MazeAlgorithmType algorithmType)
    {
        var mazeData = _mazeGenerator.GenerateMaze(width, height);

        var maze = new Maze
        {
            Width = width,
            Height = height,
            MazeDataJson = JsonConvert.SerializeObject(mazeData),
            AlgorithmType = algorithmType
        };

        var mazeResult = await _mazeRepository.AddAsync(maze);
        return mazeResult;
    }

    public async Task<List<Point2D>> SolveMazeAsync(int mazeId, Point2D start, Point2D end)
    {
        var maze = await _mazeRepository.GetByIdAsync(mazeId);
        if (maze == null || maze.MazeDataJson == null)
            return new List<Point2D>();

        var mazeData = JsonConvert.DeserializeObject<int[,]>(maze.MazeDataJson);

        if (mazeData == null)
            return new List<Point2D>();

        return _pathFinder.FindPath(mazeData, start, end);
    }

    public async Task<Maze?> GetMazeAsync(int mazeId)
    {
        var maze = await _mazeRepository.GetByIdAsync(mazeId);
        if (maze == null || maze.MazeDataJson == null)
            return null;

        var mazeData = JsonConvert.DeserializeObject<int[][]>(maze.MazeDataJson);
        return new Maze(maze.Id, maze.Width, maze.Height, mazeData ?? new int[][] { });
    }
}