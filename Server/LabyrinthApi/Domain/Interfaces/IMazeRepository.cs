using LabyrinthApi.Domain.Entities;

namespace LabyrinthApi.Domain.Interfaces;

public interface IMazeRepository
{
    Task<Maze?> GetByIdAsync(int id);
    Task<int> AddAsync(Maze maze);
}
