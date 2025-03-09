using LabyrinthApi.Application.Services;

namespace LabyrinthApi.Tests.Application.Services;

public class RecBacktrackingGeneratorTests
{
    [Fact]
    public void GenerateMaze_Should_Return_Valid_Maze()
    {
        int width = 10;
        int height = 10;
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
}