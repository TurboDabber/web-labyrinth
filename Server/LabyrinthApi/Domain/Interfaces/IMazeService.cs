using LabyrinthApi.Domain.Entities;
using LabyrinthApi.Domain.Enums;
using LabyrinthApi.Domain.Other;

namespace LabyrinthApi.Domain.Interfaces
{
    public interface IMazeService
    {
        Task<int> GenerateMazeAsync(int width, int height, MazeAlgorithmType algorithmType = MazeAlgorithmType.RecursiveBacktracking);
        Task<List<Point2D>> SolveMazeAsync(int mazeId, Point2D start, Point2D end);
        Task<Maze?> GetMazeAsync(int mazeId);
    }
}
