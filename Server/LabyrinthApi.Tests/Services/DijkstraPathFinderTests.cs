using LabyrinthApi.Application.Services;
using LabyrinthApi.Domain.Other;

namespace LabyrinthApi.Tests.Application.Services;

public class DijkstraPathFinderTests
{
    [Fact]
    public void FindPath_Should_Return_Valid_Path()
    {
        var maze = new int[,]
        {
            { 0, 1, 0 },
            { 0, 0, 0 },
            { 1, 1, 0 }
        };
        var expectedPath = new List<Point2D>
            {
                new(0, 0),
                new(0, 1),
                new(1, 1),
                new(2, 1),
                new(2, 2)
            };

        Point2D start = new(0, 0);
        Point2D end = new(2, 2);
        var pathFinder = new DijkstraPathFinder();

        var path = pathFinder.FindPath(maze, start, end);

        Assert.NotNull(path);
        Assert.NotEmpty(path);

        Assert.Equal(expectedPath, path);
    }

    [Fact]
    public void FindPath_Should_Return_Empty_Path_If_No_Solution()
    {
        var maze = new int[,]
        {
            { 0, 1, 1 },
            { 1, 1, 1 },
            { 1, 1, 0 }
        };
        Point2D start = new(0, 0);
        Point2D end = new(2, 2);
        var pathFinder = new DijkstraPathFinder();

        var path = pathFinder.FindPath(maze, start, end);

        Assert.Empty(path);
    }
}