using LabyrinthApi.Domain.Enums;
using LabyrinthApi.Domain.Interfaces;

namespace LabyrinthApi.Application.Services;

public class DijkstraPathFinder : IPathFinder
{
    public List<(int, int)> FindPath(int[,] maze, (int, int) start, (int, int) end)
    {
        int height = maze.GetLength(0);
        int width = maze.GetLength(1);

        var distances = new int[height, width];
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                distances[i, j] = int.MaxValue;
            }
        }
        distances[start.Item2, start.Item1] = 0;

        var queue = new PriorityQueue<(int, int), int>();
        queue.Enqueue(start, 0);

        while (queue.Count > 0)
        {
            var (x, y) = queue.Dequeue();

            if (x == end.Item1 && y == end.Item2)
            {
                return ReconstructPath(distances, start, end);
            }

            foreach (var direction in Enum.GetValues(typeof(Directions)))
            {
                var (newX, newY) = GetNewPosition(x, y, (Directions)direction);

                if (IsInBounds(newX, newY, width, height) && maze[newY, newX] == 0)
                {
                    int newDistance = distances[y, x] + 1;

                    if (newDistance < distances[newY, newX])
                    {
                        distances[newY, newX] = newDistance;
                        queue.Enqueue((newX, newY), newDistance);
                    }
                }
            }
        }

        return new List<(int, int)>();
    }

    private (int, int) GetNewPosition(int x, int y, Directions direction)
    {
        switch (direction)
        {
            case Directions.Up:
                return (x, y - 1);
            case Directions.Down:
                return (x, y + 1);
            case Directions.Left:
                return (x - 1, y);
            case Directions.Right:
                return (x + 1, y);
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }
    }

    private bool IsInBounds(int x, int y, int width, int height)
    {
        return x >= 0 && x < width && y >= 0 && y < height;
    }

    private List<(int, int)> ReconstructPath(int[,] distances, (int, int) start, (int, int) end)
    {
        var path = new List<(int, int)>();
        var (x, y) = end;

        while (x != start.Item1 || y != start.Item2)
        {
            path.Add((x, y));

            foreach (var direction in Enum.GetValues(typeof(Directions)))
            {
                var (newX, newY) = GetNewPosition(x, y, (Directions)direction);

                if (IsInBounds(newX, newY, distances.GetLength(1), distances.GetLength(0)) &&
                    distances[newY, newX] == distances[y, x] - 1)
                {
                    x = newX;
                    y = newY;
                    break;
                }
            }
        }

        path.Add(start);
        path.Reverse();
        return path;
    }
}