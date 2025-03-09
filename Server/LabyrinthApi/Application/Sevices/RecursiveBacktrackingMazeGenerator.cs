using LabyrinthApi.Domain.Enums;
using LabyrinthApi.Domain.Interfaces;

namespace LabyrinthApi.Application.Services;

public class RecursiveBacktrackingMazeGenerator : IMazeGenerator
{
    public int[,] GenerateMaze(int width, int height)
    {
        var maze = new int[height, width];
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                maze[i, j] = 1;
            }
        }

        var random = new Random();
        int startX = random.Next(0, width);
        int startY = random.Next(0, height);

        GenerateMazeRecursive(maze, startX, startY);

        return maze;
    }

    private void GenerateMazeRecursive(int[,] maze, int x, int y)
    {
        Directions[] directions = { Directions.Up, Directions.Down, Directions.Left, Directions.Right };
        Shuffle(directions);

        foreach (var direction in directions)
        {
            int newX = x;
            int newY = y;

            switch (direction)
            {
                case Directions.Up:
                    newY -= 2;
                    break;
                case Directions.Down:
                    newY += 2;
                    break;
                case Directions.Left:
                    newX -= 2;
                    break;
                case Directions.Right:
                    newX += 2;
                    break;
            }

            if (IsInBounds(newX, newY, maze.GetLength(1), maze.GetLength(0)) && maze[newY, newX] == 1)
            {
                maze[(y + newY) / 2, (x + newX) / 2] = 0;
                maze[newY, newX] = 0;

                GenerateMazeRecursive(maze, newX, newY);
            }
        }
    }

    private bool IsInBounds(int x, int y, int width, int height)
    {
        return x >= 0 && x < width && y >= 0 && y < height;
    }

    private void Shuffle(Directions[] array)
    {
        var random = new Random();
        for (int i = array.Length - 1; i > 0; i--)
        {
            int j = random.Next(0, i + 1);
            Directions temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
}