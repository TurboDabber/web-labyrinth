using LabyrinthApi.Application.Services;
using LabyrinthApi.Domain.Other;

namespace LabyrinthApi.Tests.Application.Services;

public class RecBacktrackingGeneratorTests
{
    [Fact]
    public void GenerateMaze_Should_Return_Valid_Maze()
    {
        int width = 11;
        int height = 11;
        var generator = new RecursiveBacktrackingMazeGenerator();

        var maze = generator.GenerateMaze(width, height);
        Assert.NotNull(maze);
        Assert.Equal(height, maze.GetLength(0));
        Assert.Equal(width, maze.GetLength(1));
        var flattenedMaze = maze.Cast<int>();
        bool hasFloor = flattenedMaze.Contains(0);
        bool hasWall = flattenedMaze.Contains(1);
        Assert.True(hasFloor);
        Assert.True(hasWall);

    }

    [Fact]
    public void GeneratedMaze_ShouldBeSolvable()
    {
        var generator = new RecursiveBacktrackingMazeGenerator();
        int width = 11;
        int height = 11;

        int[,] maze = generator.GenerateMaze(width, height);
        List<Point2D> startPoints = FindStartAndEndPoints(maze);
        Assert.NotNull(startPoints);
        Assert.Equal(2, startPoints.Count);


        if (startPoints.Count == 2)
        {
            Assert.NotEqual(-1, startPoints[0].X);
            Assert.NotEqual(-1, startPoints[0].Y);
            Assert.NotEqual(-1, startPoints[1].X);
            Assert.NotEqual(-1, startPoints[1].Y);
            int startX = startPoints[0].X;
            int startY = startPoints[0].Y;
            int endX = startPoints[1].X;
            int endY = startPoints[1].Y;

            FloodFill(maze, startX, startY);

            Assert.Equal(2, maze[endY, endX]);

            bool hasUnfilledPath = false;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (maze[y, x] == 0) hasUnfilledPath = true;
                }
            }

            Assert.False(hasUnfilledPath);
        }
        else
        {
            Assert.Fail();
        }
    }

    private void FloodFill(int[,] maze, int startX, int startY)
    {
        int width = maze.GetLength(1);
        int height = maze.GetLength(0);
        Queue<(int, int)> queue = new();
        queue.Enqueue((startX, startY));
        maze[startY, startX] = 2;

        int[] dx = { 0, 0, -1, 1 };
        int[] dy = { -1, 1, 0, 0 };

        while (queue.Count > 0)
        {
            (int x, int y) = queue.Dequeue();

            for (int i = 0; i < 4; i++)
            {
                int newX = x + dx[i];
                int newY = y + dy[i];

                if (newX >= 0 && newX < width && newY >= 0 && newY < height && maze[newY, newX] == 0)
                {
                    maze[newY, newX] = 2;
                    queue.Enqueue((newX, newY));
                }
            }
        }
    }

    List<Point2D> FindStartAndEndPoints(int[,] maze)
    {
        Point2D startPoint = new Point2D(-1,-1);
        Point2D endPoint = new Point2D(-1, -1);

        for (int i = 0; i < maze.GetLength(0); i++)
        {
            for (int j = 0; j < maze.GetLength(1); j++)
            {
                if (maze[i, j] == 0)
                {
                    if (startPoint.X == -1 && startPoint.Y == -1)
                    {
                        startPoint = new Point2D(j, i);
                    }
                    endPoint = new Point2D(j, i);
                }
            }
        }

        return new List<Point2D> { startPoint, endPoint };
    }
}