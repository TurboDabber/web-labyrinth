using LabyrinthApi.Domain.Entities;

namespace LabyrinthApi.Domain.Repositories
{
    public interface IMazeRepository
    {
        Task<Maze?> GetByIdAsync(int id);
        Task AddAsync(Maze maze);
    }
}
