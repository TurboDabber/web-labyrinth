using Xunit;
using LabyrinthApi.Domain.Enums;
using LabyrinthApi.Domain.Entities;


namespace LabyrinthTests.Entities;
public class MazeTests
{
    [Fact]
    public void Maze_Should_Have_Correct_Properties()
    {
        var maze = new Maze
        {
            Id = 1,
            Width = 10,
            Height = 10,
            MazeDataJson = "[[0, 1], [1, 0]]",
            AlgorithmType = MazeAlgorithmType.RecursiveBacktracking
        };

        Assert.Equal(1, maze.Id);
        Assert.Equal(10, maze.Width);
        Assert.Equal(10, maze.Height);
        Assert.Equal("[[0, 1], [1, 0]]", maze.MazeDataJson);
        Assert.Equal(MazeAlgorithmType.RecursiveBacktracking, maze.AlgorithmType);
    }

    [Fact]
    public void MazeData_Should_Convert_Json_To_Array()
    {
        var maze = new Maze
        {
            MazeDataJson = "[[0, 1], [1, 0]]"
        };

        var mazeData = maze.MazeData;

        Assert.NotNull(mazeData);
        Assert.Equal(0, mazeData[0, 0]);
        Assert.Equal(1, mazeData[0, 1]);
        Assert.Equal(1, mazeData[1, 0]);
        Assert.Equal(0, mazeData[1, 1]);
    }

    [Fact]
    public void MazeData_Should_Convert_Array_To_Json()
    {
        var maze = new Maze();
        var mazeData = new int[,]
        {
                { 0, 1 },
                { 1, 0 }
        };

        maze.MazeData = mazeData;

        Assert.Equal("[[0,1],[1,0]]", maze.MazeDataJson);
    }
}