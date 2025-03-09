using LabyrinthApi.Domain.Enums;
using LabyrinthApi.Domain.Interfaces;
using LabyrinthApi.Domain.Other;

namespace LabyrinthApi.Application.Services;

public class DijkstraPathFinder : IPathFinder
{
    public List<Point2D> FindPath(int[,] maze, Point2D start, Point2D end)
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
        distances[start.x, start.y] = 0;

        var queue = new PriorityQueue<Point2D, int>();
        queue.Enqueue(start, 0);

        while (queue.Count > 0)
        {
            var (x, y) = queue.Dequeue();

            if (x == end.x && y == end.y)
            {
                return ReconstructPath(distances, start, end);
            }

            foreach (var direction in Enum.GetValues(typeof(Directions)))
            {
                Point2D newPoint = GetNewPosition(x, y, (Directions)direction);

                if (IsInBounds(newPoint.x, newPoint.y, width, height) && maze[newPoint.y, newPoint.x] == 0)
                {
                    int newDistance = distances[y, x] + 1;

                    if (newDistance < distances[newPoint.y, newPoint.x])
                    {
                        distances[newPoint.y, newPoint.x] = newDistance;
                        queue.Enqueue(newPoint, newDistance);
                    }
                }
            }
        }

        return new List<Point2D>();
    }

    private Point2D GetNewPosition(int x, int y, Directions direction)
    {
        switch (direction)
        {
            case Directions.Up:
                return new(x, y - 1);
            case Directions.Down:
                return new(x, y + 1);
            case Directions.Left:
                return new(x - 1, y);
            case Directions.Right:
                return new(x + 1, y);
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }
    }

    private bool IsInBounds(int x, int y, int width, int height)
    {
        return x >= 0 && x < width && y >= 0 && y < height;
    }

    private List<Point2D> ReconstructPath(int[,] distances, Point2D start, Point2D end)
    {
        var path = new List<Point2D>();
        var x = end.x;
        var y = end.y;
        while (x != start.x || y != start.y)
        {
            path.Add(new Point2D(x, y));

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