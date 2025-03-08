using LabyrinthApi.Domain.Entities;
using LabyrinthApi.Domain.Interfaces;
using LabyrinthApi.Infrastructure.Data;

namespace LabyrinthApi.Infrastructure.Repositories;

public class DbMazeRepository : IMazeRepository
{
    private readonly MazeDbContext _context;

    public DbMazeRepository(MazeDbContext context)
    {
        _context = context;
    }

    public async Task<Maze?> GetByIdAsync(int id)
    {
        return await _context.Mazes.FindAsync(id);
    }

    public async Task<int> AddAsync(Maze maze)
    {
        var added = await _context.Mazes.AddAsync(maze);
        await _context.SaveChangesAsync();
        return added.Entity.Id;
    }
}
