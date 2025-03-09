using LabyrinthApi.Domain.Other;

namespace LabyrinthApi.Domain.Interfaces;

public interface IPathFinder
{
    List<Point2D> FindPath(int[,] maze, Point2D start, Point2D end);
}
