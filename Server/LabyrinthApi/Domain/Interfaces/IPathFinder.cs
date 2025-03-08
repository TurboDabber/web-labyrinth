namespace LabyrinthApi.Domain.Interfaces;

public interface IPathFinder
{
    List<(int, int)> FindPath(int[,] maze, (int, int) start, (int, int) end);
}
