using LabyrinthApi.Domain.Enums;
using LabyrinthApi.Domain.Interfaces;

namespace LabyrinthApi.Application.Services;

public class RecursiveBacktrackingMazeGenerator : IMazeGenerator
{
    public int[,] GenerateMaze(int width, int height)
    {
        if (width % 2 == 0) width++;
        if (height % 2 == 0) height++;

        var maze = new int[height, width];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                maze[y, x] = 1;
            }
        }

        var random = new Random();

        int startX = random.Next(1, width / 2) * 2 - 1;
        int startY = random.Next(1, height / 2) * 2 - 1;

        maze[startY, startX] = 0;

        GenerateMazeRecursive(maze, startX, startY, random);

        return maze;
    }

    private void GenerateMazeRecursive(int[,] maze, int x, int y, Random random)
    {
        Directions[] directions = { Directions.Up, Directions.Down, Directions.Left, Directions.Right };
        directions = directions.OrderBy(d => random.Next()).ToArray();

        foreach (var direction in directions)
        {
            int newX = x, newY = y;

            switch (direction)
            {
                case Directions.Up: newY -= 2; break;
                case Directions.Down: newY += 2; break;
                case Directions.Left: newX -= 2; break;
                case Directions.Right: newX += 2; break;
            }

            if (IsInBounds(newX, newY, maze) && maze[newY, newX] == 1)
            {
                maze[(y + newY) / 2, (x + newX) / 2] = 0;
                maze[newY, newX] = 0;

                GenerateMazeRecursive(maze, newX, newY, random);
            }
        }
    }

    private bool IsInBounds(int x, int y, int[,] maze)
    {
        return x > 0 && x < maze.GetLength(1) - 1 && y > 0 && y < maze.GetLength(0) - 1;
    }
}
