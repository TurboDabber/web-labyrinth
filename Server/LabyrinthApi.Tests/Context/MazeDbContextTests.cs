using LabyrinthApi.Domain.Entities;
using LabyrinthApi.Domain.Enums;
using LabyrinthApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LabyrinthTests.Context;

public class MazeDbContextTests
{
    [Fact]
    public async Task MazeDbContext_Should_Save_And_Retrieve_Maze()
    {
        var options = new DbContextOptionsBuilder<MazeDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using (var context = new MazeDbContext(options))
        {
            var maze = new Maze
            {
                Width = 10,
                Height = 10,
                MazeDataJson = "[[0, 1], [1, 0]]",
                AlgorithmType = MazeAlgorithmType.RecursiveBacktracking
            };

            context.Mazes.Add(maze);
            await context.SaveChangesAsync();
        }

        using (var context = new MazeDbContext(options))
        {
            var savedMaze = await context.Mazes.FirstOrDefaultAsync();
            Assert.NotNull(savedMaze);
            Assert.Equal(10, savedMaze.Width);
            Assert.Equal(10, savedMaze.Height);
            Assert.Equal("[[0, 1], [1, 0]]", savedMaze.MazeDataJson);
            Assert.Equal(MazeAlgorithmType.RecursiveBacktracking, savedMaze.AlgorithmType);
        }
    }
}

